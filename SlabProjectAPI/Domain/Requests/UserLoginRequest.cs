﻿using System.ComponentModel.DataAnnotations;

namespace SlabProjectAPI.Domain.Requests
{
    public class UserLoginRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}