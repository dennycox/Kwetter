using AuthenticationService.Data;
using AuthenticationService.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.InMemoryData;
using Test.Messaging;

namespace Test.WebApplicationFactory
{
    public class CustomWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase(databaseName: "authentication-service-identity-integration-test-db"));
                services.AddSingleton<IConnectionProvider>(new MockConnectionProvider());
                services.AddScoped<IPublisher>(x => new MockPublisher(x.GetService<IConnectionProvider>()));
                InMemoryIntegrationTestDataGenerator.Initialize(services.BuildServiceProvider());
            });
        }
    }
}
