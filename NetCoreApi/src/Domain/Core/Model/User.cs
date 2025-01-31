﻿using System;
using Microsoft.AspNetCore.Identity;

namespace NetCoreExampleAuth.Domain.Core.Model
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
