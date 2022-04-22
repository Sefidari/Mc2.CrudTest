using Mc2.CrudTest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.AcceptanceTests.MockData
{
    public class CustomerMockData
    {
        public static List<Customer> GetCustomers()
        {
            return new List<Customer>
            {
                new Customer
                {
                    Id = 1,
                    FirstName = "Angelina",
                    LastName = "Jolie",
                    DateOfBirth = Convert.ToDateTime("1975-4-1"),
                    Email = "Angelina@gmail.com",
                    BankAccountNumber = "5105105105105100",
                    PhoneNumber = "09125971157"
                },
                new Customer
                {
                    Id = 1,
                    FirstName = "Brad",
                    LastName = "Pitt",
                    DateOfBirth = Convert.ToDateTime("1963-8-1"),
                    Email = "Pitt@gmail.com",
                    BankAccountNumber = "4012888888881881",
                    PhoneNumber = "09125971158"
                }
            };
        }

        public static List<Customer> GetEmptyCustomers()
        {
            return new List<Customer>();
        }

        public static Customer NewCustomer()
        {
            return new Customer
            {
                Id = 0,
                FirstName = "Amanda",
                LastName = "Pitt",
                DateOfBirth = Convert.ToDateTime("2010-8-1"),
                Email = "Amanda@gmail.com",
                BankAccountNumber = "5555555555554444",
                PhoneNumber = "09125971159"
            };
        }
    }
}
