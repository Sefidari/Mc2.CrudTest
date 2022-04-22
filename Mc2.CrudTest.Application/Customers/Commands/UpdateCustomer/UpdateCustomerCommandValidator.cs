using FluentValidation;
using Mc2.CrudTest.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            //RuleFor(v => v)
            //    .MustAsync(BeUniqueInfo).WithMessage("The specified FirstName, LastName and DateOfBirth is duplicated.");
        }

        public async Task<bool> BeUniqueEmail(UpdateCustomerCommand customer, string email, CancellationToken cancellationToken)
        {
            return await _context.Customers
                .Where(c => c.Id != customer.Id)
                .AllAsync(c => c.Email != email, cancellationToken);
        }

        public async Task<bool> BeUniqueInfo(UpdateCustomerCommand customer, CancellationToken cancellationToken)
        {
            return await _context.Customers
                .Where(c => c.Id != customer.Id)
                .AllAsync(c => c.FirstName != customer.FirstName && c.LastName != customer.LastName
                    && c.DateOfBirth != customer.DateOfBirth, cancellationToken);
        }

        public bool BeValidPhoneNumber(string phone)
        {
            bool isValidNumber = false;

            PhoneNumberUtil phoneUtil = PhoneNumberUtil.GetInstance();
            try
            {
                string countryCode = "US";
                PhoneNumbers.PhoneNumber phoneNumber = phoneUtil.Parse(phone, countryCode);

                //bool isMobile = false;
                isValidNumber = phoneUtil.IsValidNumber(phoneNumber); // returns true for valid number    

                // returns trueor false w.r.t phone number region  
                //bool isValidRegion = phoneUtil.IsValidNumberForRegion(phoneNumber, countryCode);

                //string region = phoneUtil.GetRegionCodeForNumber(phoneNumber); // GB, US , PK    

                //var numberType = phoneUtil.GetNumberType(phoneNumber); // Produces Mobile , FIXED_LINE    

                //string phoneNumberType = numberType.ToString();

                //if (!string.IsNullOrEmpty(phoneNumberType) && phoneNumberType == "MOBILE")
                //{
                //    isMobile = true;
                //}

                //var originalNumber = phoneUtil.Format(phoneNumber, PhoneNumberFormat.E164); // Produces "+923336323997"    
            }
            catch (NumberParseException ex)
            {
                string errorMessage = "NumberParseException was thrown: " + ex.Message.ToString();
            }
            return isValidNumber;
        }

    }
}
