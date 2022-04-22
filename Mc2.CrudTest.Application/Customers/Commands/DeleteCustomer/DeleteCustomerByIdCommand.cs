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
    public class DeleteCustomerByIdCommand : IRequest<int>
    {
        public int Id { get; set; }
        public class DeleteCustomerByIdCommandHandler : IRequestHandler<DeleteCustomerByIdCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public DeleteCustomerByIdCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(DeleteCustomerByIdCommand request, CancellationToken cancellationToken)
            {
                var customer = await _context.Customers.Where(a => a.Id == request.Id).FirstOrDefaultAsync();
                //if (customer == null) return default;

                if (customer == null)
                {
                    throw new NotFoundException(nameof(Customer), request.Id);
                }

                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync(cancellationToken);
                return customer.Id;
                //return Unit.Value;
            }
        }
    }
}
