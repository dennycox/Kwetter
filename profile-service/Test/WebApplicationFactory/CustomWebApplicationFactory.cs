using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProfileService.Data;
using ProfileService.Interfaces;
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
                services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase(databaseName: "profile-service-integration-test-db"));
                services.AddSingleton<MockMessageHandlerRepository>();
                services.AddSingleton<IConnectionProvider>(new MockConnectionProvider());
                services.AddScoped<IPublisher>(x => new MockPublisher());
                services.AddTransient<ISubscriber>(x => new MockSubscriber());
                InMemoryIntegrationTestDataGenerator.Initialize(services.BuildServiceProvider());
            });
        }
    }
}
