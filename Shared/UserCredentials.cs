﻿using System.ComponentModel.DataAnnotations;

namespace StellarJadeManager.Shared
{
    public class UserCredentials
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }
    }

}