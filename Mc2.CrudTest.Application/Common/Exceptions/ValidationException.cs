using System.Collections.Generic;
using ApplicationException = Mc2.CrudTest.Domain.Exceptions.ApplicationException;

namespace Mc2.CrudTest.Application.Common.Exceptions
{
    public sealed class ValidationException : ApplicationException
    {
        public ValidationException(IDictionary<string, string[]> errorsDictionary)
            : base("Validation Failure", "One or more validation errors occurred")
            => ErrorsDictionary = errorsDictionary;

        public IDictionary<string, string[]> ErrorsDictionary { get; }
    }
}
