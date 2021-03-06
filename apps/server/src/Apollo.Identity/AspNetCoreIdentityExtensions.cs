using System;
using System.Security.Claims;
using System.Text;

using Apollo.Core.Models;
using Apollo.Identity.Services;
using Apollo.Identity.Store;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Apollo.Identity
{
    public static class AspNetCoreIdentityExtensions
    {
        public static IServiceCollection AddApolloIdentity(this IServiceCollection services, IConfiguration config)
        {
            // Identity
            services.AddIdentity<AppUser, AppRole>()
                    .AddDefaultTokenProviders();

            services.Configure(ConfigureIdentity());

            services.AddTransient<IUserStore<AppUser>, AppUserStore>()
                    .AddTransient<IRoleStore<AppRole>, AppRoleStore>();

            // JWT
            services.AddAuthentication(ConfigureAuthentication())
                    .AddJwtBearer(ConfigureJwtBearer(config));

            services.AddSingleton<IJsonWebTokenGenerator, JsonWebTokenGenerator>();

            return services;
        }

        private static Action<IdentityOptions> ConfigureIdentity()
        {
            return options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            };
        }

        private static Action<AuthenticationOptions> ConfigureAuthentication()
        {
            return options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            };
        }

        private static Action<JwtBearerOptions> ConfigureJwtBearer(IConfiguration config)
        {
            return options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = config["jwt:issuer"],

                    ValidateAudience = true,
                    ValidAudience = config["jwt:issuer"],

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["jwt:key"])),

                    RequireExpirationTime = true,
                    NameClaimType = ClaimTypes.NameIdentifier,
                };
            };
        }
    }
}
