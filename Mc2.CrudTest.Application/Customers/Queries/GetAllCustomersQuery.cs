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

namespace Mc2.CrudTest.Application.Customers.Queries
{
    public class GetAllCustomersQuery : IRequest<IEnumerable<Customer>>
    {

        public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, IEnumerable<Customer>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllCustomersQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<Customer>> Handle(GetAllCustomersQuery query, CancellationToken cancellationToken)
            {
                var customerList = await _context.Customers.ToListAsync();
                if (customerList == null)
                {
                    return null;
                }
                return customerList.AsReadOnly();
            }
        }
    }
}
