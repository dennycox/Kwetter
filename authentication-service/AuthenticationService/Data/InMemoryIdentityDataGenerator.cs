﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace AuthenticationService.Data
{
    public static class InMemoryIdentityDataGenerator
    {
        public static async Task SeedUsersAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    Id = "42048812-decb-4e22-89d7-3dcb78a2645f",
                    Name = "Denny",
                    Email = "denny@test.com",
                };

                var user2 = new AppUser
                {
                    Id = "39f2dbde-8642-4d11-9e3c-7a0fd0ebe20a",
                    Name = "Denny2",
                    Email = "denny2@test.com",
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
                await userManager.CreateAsync(user2, "Pa$$w0rd");
            }

        }
    }
}
