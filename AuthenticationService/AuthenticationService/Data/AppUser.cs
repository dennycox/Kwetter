using System;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationService.Data
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
