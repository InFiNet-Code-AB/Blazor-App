﻿using System.ComponentModel.DataAnnotations;

namespace Shared_Layer.DTO_s.User
{
    public class LoginUserDTO
    {
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        //change bransh
    }
}
