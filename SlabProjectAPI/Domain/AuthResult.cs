﻿using System.Collections.Generic;

namespace SlabProjectAPI.Domain
{
    public class AuthResult
    {
        public string Token { get; set; }
        public bool Result { get; set; }
        public List<string> Errors { get; set; }
    }
}