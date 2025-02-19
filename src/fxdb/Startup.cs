﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using fxdb.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using fxdb.Policies;
using Microsoft.AspNetCore.Authorization;

namespace fxdb
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            if (!Directory.Exists("storage")) Directory.CreateDirectory("storage");

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            var connection = Configuration.GetConnectionString("DefaultConnection") ?? @"Data Source=fxdb.sqlite";

            if (connection.Contains("sqlite")) {
                Console.WriteLine("Note - Currently using SQLite, try to keep this to dev only...");
                services.AddDbContext<FxContext>(options => options.UseSqlite(connection));
            } else {
                services.AddDbContext<FxContext>(options => options.UseMySql(connection));
            }


            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddMvc();

            // DI for fxdb - Repositories
            //services.AddSingleton<IEffectRepository, MockEffectRepository>();
            services.AddSingleton<IEffectRepository, EntityFrameworkEffectRepository>();
        
            // DI for fxdb - Policies/Policy handlers
            services.AddSingleton<IAuthorizationHandler, EmailDomainHandler >();


            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AllowedDomain", policy => policy.Requirements.Add(new EmailDomainRequirement(new List<string> { "calvaryftl.org", "student.ccaeagles.org" })));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            {
                Authority = "https://auth.nofla.me",
                RequireHttpsMetadata = true,
                ApiName = "fxdb.client"
            });
            app.UseApplicationInsightsRequestTelemetry();

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}
