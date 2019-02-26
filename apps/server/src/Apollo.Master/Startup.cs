using System;

using Apollo.Database;
using Apollo.Identity;
using Apollo.Master.Controllers.Filters;
using Apollo.Migrations;
using Apollo.Swagger;

using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Apollo.Master
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApolloDatabase()
                    .AddApolloIdentity(_config)
                    .AddApolloMigrations(_config)
                    .AddApolloSwagger();

            services.AddAutoMapper();

            services.AddMvc(ConfigureMvc());
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseApolloSwagger();
            }

            app.UseAuthentication()
               .UseMvc();
        }

        private Action<MvcOptions> ConfigureMvc()
        {
            return options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                             .RequireAuthenticatedUser()
                             .Build();

                options.Filters.Add(new AuthorizeFilter(policy));
                options.Filters.Add(new ExceptionHandlerFilterAttribute());
                options.Filters.Add(new ModelStateValidationFilterAttribute());
            };
        }
    }
}
