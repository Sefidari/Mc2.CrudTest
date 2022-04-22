using Mc2.CrudTest.Application.Interfaces;
using Mc2.CrudTest.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Application.Customers.Commands
{
    public class CreateCustomerCommand : IRequest<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }

        public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int>
        {
            private readonly IApplicationDbContext _context;

            public CreateCustomerCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
            {
                var customer = new Customer()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    BankAccountNumber = request.BankAccountNumber,
                    DateOfBirth = request.DateOfBirth
                };

                _context.Customers.Add(customer);

                await _context.SaveChangesAsync(cancellationToken);

                return customer.Id;
            }
        }
    }
}
