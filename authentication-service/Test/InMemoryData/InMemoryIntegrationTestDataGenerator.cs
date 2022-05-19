using AuthenticationService.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.InMemoryData
{
    public class InMemoryIntegrationTestDataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var user = new AppUser
                {
                    Id = "42048812-decb-4e22-89d7-3dcb78a2645f",
                    Name = "Denny",
                    Email = "denny@test.com",
                    UserName = "denny@test.com",
                };

                var user2 = new AppUser
                {
                    Id = "39f2dbde-8642-4d11-9e3c-7a0fd0ebe20a",
                    Name = "Cox",
                    Email = "cox@test.com",
                    UserName = "cox@test.com",
                };

                userManager.CreateAsync(user, "Pa$$w0rd");
                userManager.CreateAsync(user2, "Pa$$w0rd");
            }
        }
    }
}
