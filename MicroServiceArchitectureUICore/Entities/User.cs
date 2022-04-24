﻿using Microsoft.AspNetCore.Identity;

namespace MicroServiceArchitectureUIDomain.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsSeller { get; set; }
        public bool IsBuyer { get; set; }
        public bool IsAdmin { get; set; }
    }
}
