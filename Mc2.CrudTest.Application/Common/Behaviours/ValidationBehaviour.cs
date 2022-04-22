using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
//using Mc2.CrudTest.Application.Abstractions.Messaging;
using FluentValidation;
using MediatR;
using ValidationException = Mc2.CrudTest.Application.Common.Exceptions.ValidationException;


namespace Mc2.CrudTest.Application.Common.Behaviours
{
    /*
        public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : class, ICommand<TResponse>
     */
    public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (!_validators.Any())
            {
                return await next();
            }

            var context = new ValidationContext<TRequest>(request);

            var errorsDictionary = _validators
                .Select(x => x.Validate(context))
                .SelectMany(x => x.Errors)
                .Where(x => x != null)
                .GroupBy(
                    x => x.PropertyName,
                    x => x.ErrorMessage,
                    (propertyName, errorMessages) => new
                    {
                        Key = propertyName,
                        Values = errorMessages.Distinct().ToArray()
                    })
                .ToDictionary(x => x.Key, x => x.Values);

            if (errorsDictionary.Any())
            {
                throw new ValidationException(errorsDictionary);
            }

            return await next();
        }
    }

    //public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    //     where TRequest : notnull
    //{
    //    private readonly IEnumerable<IValidator<TRequest>> _validators;

    //    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    //    {
    //        _validators = validators;
    //    }

    //    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    //    {
    //        if (_validators.Any())
    //        {
    //            var context = new ValidationContext<TRequest>(request);

    //            var validationResults = await Task.WhenAll(
    //                _validators.Select(v =>
    //                    v.ValidateAsync(context, cancellationToken)));

    //            var failures = validationResults
    //                .Where(r => r.Errors.Any())
    //                .SelectMany(r => r.Errors)
    //                .ToList();

    //            if (failures.Any())
    //                throw new ValidationException(failures);
    //        }
    //        return await next();
    //    }
    //}

}
