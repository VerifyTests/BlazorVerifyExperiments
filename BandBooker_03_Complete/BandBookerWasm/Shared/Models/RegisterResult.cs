﻿using System.Collections.Generic;

namespace BandBookerWasm.Shared.Models
{
    public class RegisterResult
    {
        public bool Successful { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}