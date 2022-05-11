using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProfileService.Data;
using Microsoft.EntityFrameworkCore;
using ProfileService.Extensions;
using ProfileService.Messaging;
using ProfileService.Interfaces;
using RabbitMQ.Client;

namespace ProfileService
{
    public class Startup
    {
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _hosting;
        private readonly string AllowOrigins = "_AllowOrigins";
        public Startup(IConfiguration config, IWebHostEnvironment hosting)
        {
            _config = config;
            _hosting = hosting;

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddControllers();
            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(_config.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0))));


            services.AddHostedService<QueueReaderService>();
            services.AddSingleton<MessageHandlerRepository>();
            services.AddSingleton<IConnectionProvider>(new RabbitMqConnection(_config.GetConnectionString("RabbitMqConnectionString")));
            services.AddScoped<IPublisher>(x => new Publisher(x.GetService<IConnectionProvider>(),
                    "profile_exchange",
                    ExchangeType.Topic,
                    30000
                ));
            services.AddTransient<ISubscriber>(x => new Subscriber(x.GetService<IConnectionProvider>(),
                    "account_exchange",
                    "profile_queue",
                    "account.*",
                    ExchangeType.Topic));

            services.AddApplicationServices();
            services.AddSwaggerDocumentation();

            services.AddCors(options =>
            {
                options.AddPolicy(name: AllowOrigins,
                    builder =>
                    {
                        builder.SetIsOriginAllowed(origin => true)
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                    });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            //app.UseHttpsRedirection();

            app.UseRouting();
            app.UseStaticFiles();

            app.UseCors(AllowOrigins);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwaggerDocumentation();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
