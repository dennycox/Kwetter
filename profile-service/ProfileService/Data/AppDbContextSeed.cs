using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ProfileService.Data
{
    public class AppDbContextSeed
    {
        public static async Task SeedAsync(AppDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.Profiles.Any())
                {
                    var profile = new Profile
                    {
                        Id = new Guid("b6f8c4bc-1b8d-4661-b129-a4e51f34de87"),
                        UserId = "42048812-decb-4e22-89d7-3dcb78a2645f",
                        ProfilePictureUrl = "",
                        Name = "Denny",
                        Bio = "Hi",
                        Location = "Netherlands",
                        Website = "www.denny.nl",
                    };

                    var profile2 = new Profile
                    {
                        Id = new Guid("08b21e72-0877-4cdc-b8c7-9f2c734d2a9c"),
                        UserId = "39f2dbde-8642-4d11-9e3c-7a0fd0ebe20a",
                        ProfilePictureUrl = "",
                        Name = "Cox",
                        Bio = "Hi again",
                        Location = "Netherlands",
                        Website = "www.cox.nl",
                    };

                    context.Profiles.Add(profile);
                    context.Profiles.Add(profile2);

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<AppDbContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}
