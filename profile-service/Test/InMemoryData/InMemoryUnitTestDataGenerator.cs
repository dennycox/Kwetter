using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProfileService.Data;
using System;
using System.Threading.Tasks;

namespace Test.InMemoryData
{
    public static class InMemoryUnitTestDataGenerator
    {
        public static async Task<AppDbContext> Initialize()
        {
            var options = CreateNewContextOptions();
            var context = new AppDbContext(options);

            var profile = new Profile
            {
                Id = new Guid("b6f8c4bc-1b8d-4661-b129-a4e51f34de87"),
                UserId = "42048812-decb-4e22-89d7-3dcb78a2645f",
                ProfilePictureUrl = "",
                Name = "Denny",
                Bio = "Hello",
                Location = "Netherlands",
                Website = "www.denny.com",
            };

            var profile2 = new Profile
            {
                Id = new Guid("08b21e72-0877-4cdc-b8c7-9f2c734d2a9c"),
                UserId = "39f2dbde-8642-4d11-9e3c-7a0fd0ebe20a",
                ProfilePictureUrl = "",
                Name = "Cox",
                Bio = "World!",
                Location = "Netherlands",
                Website = "www.cox.com",
            };

            context.Profiles.Add(profile);
            context.Profiles.Add(profile2);

            await context.SaveChangesAsync();

            return context;
        }

        public static AppDbContext InitializeEmptyDB()
        {
            var options = CreateNewContextOptions();
            var context = new AppDbContext(options);
            return context;
        }

        private static DbContextOptions<AppDbContext> CreateNewContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseInMemoryDatabase(databaseName: "profile-service-unit-test-db")
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

    }
}
