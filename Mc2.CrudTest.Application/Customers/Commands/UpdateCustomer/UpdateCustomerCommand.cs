using FluentValidation;
using Mc2.CrudTest.Application.Common;
using Mc2.CrudTest.Application.Interfaces;
using Mc2.CrudTest.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Application.Customers.Commands
{
    public class UpdateCustomerCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }

        public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, int>
        {
            private readonly IApplicationDbContext _context;
            //private readonly IValidator<UpdateCustomerCommand> _validator;

            public UpdateCustomerCommandHandler(IApplicationDbContext context) //, IValidator<UpdateCustomerCommand> validator
            {
                _context = context;
                //_validator = validator;
            }
            public async Task<int> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
            {
                //var result = _validator.Validate(request);

                //if (!result.IsValid)
                //{
                //    var errors = result.Errors.Select(x => x.ErrorMessage).ToArray();
                //    throw new InvalidRequestBodyException
                //    {
                //        Errors = errors
                //    };
                //}

                var customer = await _context.Customers.FirstOrDefaultAsync(a => a.Id == request.Id);
                //var customer = await _context.Customers.FindAsync(new object[] { request.Id }, cancellationToken);

                if (customer == null)
                {
                    //return default;
                    throw new NotFoundException(nameof(Customer), request.Id);
                }

                customer.FirstName = request.FirstName;
                customer.LastName = request.LastName;
                customer.PhoneNumber = request.PhoneNumber;
                customer.DateOfBirth = request.DateOfBirth;
                customer.BankAccountNumber = request.BankAccountNumber;
                customer.Email = request.Email;

                await _context.SaveChangesAsync(cancellationToken);

                return customer.Id;
            }
        }
    }
}
