﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Remote = System.Web.Mvc.RemoteAttribute;

namespace LazyVocabulary.WEB.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "UserName")]
        [Remote("IsUserNameAvailable", "Account")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        [Remote("IsEmailAvailable", "Account")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}