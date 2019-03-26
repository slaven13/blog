﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessLogic.Models.Account
{
    public class ResetPasswordModel
    {        
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Username { get; set; }

        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirm password must be the same. They don't match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}
