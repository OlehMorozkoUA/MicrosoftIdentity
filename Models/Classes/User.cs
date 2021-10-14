﻿using Microsoft.AspNetCore.Identity;

namespace Models.Classes
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
