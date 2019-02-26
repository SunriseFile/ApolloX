using Apollo.Database.Repositories;

using Microsoft.Extensions.DependencyInjection;

namespace Apollo.Database
{
    public static class AspNetCoreDatabaseExtensions
    {
        public static IServiceCollection AddApolloDatabase(this IServiceCollection services)
        {
            services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>()
                    .AddSingleton<IUserRepository, UserRepository>();

            return services;
        }
    }
}
