using FluentValidation;
using Mc2.CrudTest.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using PhoneNumbers;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Application.Customers.Commands
{
    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateCustomerCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.FirstName)
                .NotEmpty().WithMessage("FirstName is required.");

            RuleFor(v => v.LastName)
                .NotEmpty().WithMessage("LastName is required.");

            RuleFor(v => v.PhoneNumber)
                .NotEmpty().WithMessage("PhoneNumber is required.")
                .MaximumLength(20).WithMessage("PhoneNumber must not exceed 20 characters.")
                .Must(BeValidPhoneNumber).WithMessage("The specified PhoneNumber is not valid.");

            RuleFor(v => v.Email)
                .NotEmpty().WithMessage("Email is required.")
                .MaximumLength(320).WithMessage("Email must not exceed 320 characters.")
                .MustAsync(BeUniqueEmail).WithMessage("The specified email already exists.");

            RuleFor(v => v)
                .MustAsync(BeUniqueInfo).WithMessage("Customer By this Firstname, Lastname and DateOfBirth already exists.");
        }

        public async Task<bool> BeUniqueEmail(UpdateCustomerCommand customer, string email, CancellationToken cancellationToken)
        {
            return await _context.Customers
                .Where(c => c.Id != customer.Id)
                .AllAsync(c => c.Email != email, cancellationToken);
        }

        public async Task<bool> BeUniqueInfo(UpdateCustomerCommand customer, CancellationToken cancellationToken)
        {
            var result = await _context.Customers
                .Where(c => c.Id != customer.Id)
                .AnyAsync(c => c.FirstName == customer.FirstName && c.LastName == customer.LastName
                    && c.DateOfBirth == customer.DateOfBirth, cancellationToken);
            return !result;
        }

        public bool BeValidPhoneNumber(string phone)
        {
            bool isValidNumber = false;

            PhoneNumberUtil phoneUtil = PhoneNumberUtil.GetInstance();
            try
            {
                string countryCode = "PK";
                PhoneNumbers.PhoneNumber phoneNumber = phoneUtil.Parse(phone, countryCode);

                isValidNumber = phoneUtil.IsValidNumber(phoneNumber); // returns true for valid number     
            }
            catch (NumberParseException ex)
            {
                isValidNumber = false;
            }
            return isValidNumber;
        }

    }
}
