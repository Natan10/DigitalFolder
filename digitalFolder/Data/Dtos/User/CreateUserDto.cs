﻿using System.ComponentModel.DataAnnotations;

namespace DigitalFolder.Data.Dtos.User
{
    public class CreateUserDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "RePassword is not valid")]
        public string RePassword { get; set; }
    }
}
