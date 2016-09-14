using System;
using System.Configuration;
using System.IO;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.Swagger.Model;
using SwashbuckleExample.AuthorizationRequirements;
using SwashbuckleExample.core.interfaces;
using SwashbuckleExample.db;
using SwashbuckleExample.MiddleWare;

namespace SwashbuckleExample
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)                
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            
            Configuration = builder.Build();

            // Set up data directory
            string appRoot = PlatformServices.Default.Application.ApplicationBasePath;

            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(appRoot, "App_Data"));
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddMemoryCache();

            //configure ip rate limiting middle-ware
            services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));
            services.Configure<IpRateLimitPolicies>(Configuration.GetSection("IpRateLimitPolicies"));
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();

            //configure client rate limiting middleware
            services.Configure<ClientRateLimitOptions>(Configuration.GetSection("ClientRateLimiting"));
            services.Configure<ClientRateLimitPolicies>(Configuration.GetSection("ClientRateLimitPolicies"));
            services.AddSingleton<IClientPolicyStore, MemoryCacheClientPolicyStore>();
            //services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();

            //configure client rate limiting middleware
            services.Configure<UserRateLimitOptions>(Configuration.GetSection("UserRateLimiting"));
            services.Configure<UserRateLimitPolicies>(Configuration.GetSection("UserRateLimitPolicies"));
            services.AddSingleton<IUserPolicyStore, MemoryCacheUserPolicyStore>();

            var opt = new ClientRateLimitOptions();
            ConfigurationBinder.Bind(Configuration.GetSection("ClientRateLimiting"), opt);

            var optUser = new UserRateLimitOptions();
            ConfigurationBinder.Bind(Configuration.GetSection("UserRateLimiting"), optUser);

            services.AddTransient<IPeopleRepository, PeopleRepository>();
            services.AddTransient<IPersonCarRepository, PersonCarRepository>();
            services.AddTransient<ICarRepository, CarRepository>();

            // Add DbContext
            var conStr = Configuration["Data:ApplicationDb:ConnectionString"];
            services.AddScoped((_) => new ApplicationDbContext(conStr));

            // Add framework services.
            //services.AddMvc();
            services.AddMvc().AddJsonOptions(options => {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            
            services.AddSwaggerGen();

            services.ConfigureSwaggerGen(options =>
            {
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "Swashbuckle Test API",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "Darren Schwarz", Email = "darrenschwarz@gmail.com", Url = "http://www.darrenschwarz.com" },
                    License = new License { Name = "Use under LICX", Url = "http://url.com" }
                });

                //Determine base path for the application.
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;

                //Set the comments path for the swagger json and ui.
                options.IncludeXmlComments(basePath + "\\SwashbuckleExample.xml");
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("IOAdmin", policy => policy.Requirements.Add(new RoleRequirement("IOAdmin")));
                options.AddPolicy("SomeOther", policy => policy.Requirements.Add(new RoleRequirement("SomeOther")));
            });

            services.AddSingleton<IAuthorizationHandler, RoleHandler>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.EnvironmentName == "TestServer" || env.EnvironmentName == "DevelopmentProject")
                app.UseNonIisWindowsIdentityMiddleWare();

            app.UseRoleMiddleWare();
            app.UseIpRateLimiting();
            app.UseUserRateimiterMiddleWare();
            
            //app.UseClientRateLimiting();
            
       
            //app.UseMvcWithDefaultRoute();
            app.UseMvc();

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            app.UseSwaggerUi();
        }
    }
}
