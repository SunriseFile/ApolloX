using System.Collections.Generic;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using Swashbuckle.AspNetCore.Swagger;

namespace Apollo.Swagger
{
    public static class AspNetCoreSwaggerExtensions
    {
        public static IServiceCollection AddApolloSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Title = "Apollo",
                    Version = "v1"
                });

                options.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Name = "Authorization",
                    Description = "Bearer token",
                    In = "Header",
                    Type = "apiKey"
                });

                options.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    ["Bearer"] = new List<string>()
                });
            });

            return services;
        }

        public static IApplicationBuilder UseApolloSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger()
               .UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Apollo API"));

            return app;
        }
    }
}
