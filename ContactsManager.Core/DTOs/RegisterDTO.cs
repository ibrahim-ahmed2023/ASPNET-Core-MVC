﻿
using System.ComponentModel.DataAnnotations;

using ContactsManager.Core.Enums;

namespace ContactsManager.Core.DTOs
{

    public class RegisterDTO
    {
        [Required(ErrorMessage = "Name can't be blank")]
        public string PersonName { get; set; }


        [Required(ErrorMessage = "Email can't be blank")]
        [EmailAddress(ErrorMessage = "Email should be in a proper email address format")]
        //[Remote(action: "IsEmailAlreadyRegistered", controller: "Account", ErrorMessage = "email is already used")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Phone can't be blank")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone number should contain numbers only")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }


        [Required(ErrorMessage = "Password can't be blank")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "Confirm Password can't be blank")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password And confirm Password don't match")]
        public string ConfirmPassword { get; set; }

        public UserTypeOptions UserType { get; set; } = UserTypeOptions.User;
    }
}
