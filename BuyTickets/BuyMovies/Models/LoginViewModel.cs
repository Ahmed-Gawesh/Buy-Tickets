﻿using System.ComponentModel.DataAnnotations;

namespace BuyMovies.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email Name Is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password Is Required")]
        [DataType(DataType.Password)]//*********
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
