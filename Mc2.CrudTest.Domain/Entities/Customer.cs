using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "First Name is too long.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Last Name is too long.")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Phone]
        [StringLength(20, ErrorMessage = "Phone Number is too long.")]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(320, ErrorMessage = "Phone Number is too long.")]
        public string Email { get; set; }

        [Required]
        [CreditCard]
        [StringLength(50, ErrorMessage = "Phone Number is too long.")]
        public string BankAccountNumber { get; set; }
    }
}
