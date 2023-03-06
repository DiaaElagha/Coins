using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Coins.Core.Models.DtoAPI.Auth
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Please Enter FirstName")]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string GrandfatherName { get; set; }
        [Required(ErrorMessage = "Please Enter LastName")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please Enter UserName")]
        public string UserName { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Enter Valid Format Email")]
        public string Email { get; set; }

        public string Address { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Please Enter Mobile Number")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Please enter a 10-digit number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        public string Password { get; set; }

        [Required]
        public string FcmToken { get; set; }
    }
}
