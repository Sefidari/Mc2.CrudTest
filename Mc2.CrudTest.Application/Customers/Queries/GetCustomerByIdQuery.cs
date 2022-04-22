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
    public class GetCustomerByIdQuery : IRequest<Customer>
    {
        public int Id { get; set; }
        public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Customer>
        {
            private readonly IApplicationDbContext _context;
            public GetCustomerByIdQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<Customer> Handle(GetCustomerByIdQuery query, CancellationToken cancellationToken)
            {
                var customer = await _context.Customers.Where(a => a.Id == query.Id).FirstOrDefaultAsync();
                if (customer == null) return null;
                return customer;

        //        return await _context.Customers
        //.Where(c => c.Id == request.Id)
        //.AsNoTracking()
        //.ProjectTo<CustomerVM>(_mapper.ConfigurationProvider)
        //.OrderBy(t => t.LastName)
        //.FirstOrDefaultAsync(cancellationToken);
            }
        }
    }
}
