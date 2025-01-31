﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreExampleAuth.Patterns.Contracts.Authentication
{
    public class UserLoginResponseContract
    {
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string UserFullName { get; set; }

        public string Token { get; set; }

        public string[] Roles { get; set; }
    }
}
