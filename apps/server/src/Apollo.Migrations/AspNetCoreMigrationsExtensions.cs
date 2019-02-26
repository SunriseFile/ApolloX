using System;

using FluentMigrator.Builders.Create.Table;
using FluentMigrator.Runner;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Apollo.Migrations
{
    public static class AspNetCoreMigrationsExtensions
    {
        public static IServiceCollection AddApolloMigrations(this IServiceCollection services, IConfiguration config)
        {
            services.AddFluentMigratorCore()
                    .ConfigureRunner(ConfigureMigrationRunner(config));

            services.AddSingleton<IDatabaseInitializer, DatabaseInitializer>();

            return services;
        }

        internal static ICreateTableColumnOptionOrWithColumnSyntax WithIdColumn(this ICreateTableWithColumnSyntax columnSyntax)
        {
            return columnSyntax.WithColumn("Id")
                               .AsInt32()
                               .NotNullable()
                               .PrimaryKey()
                               .Identity();
        }

        private static Action<IMigrationRunnerBuilder> ConfigureMigrationRunner(IConfiguration config)
        {
            return builder => builder.AddMySql5()
                                     .WithGlobalConnectionString(config.GetConnectionString("mysql"))
                                     .ScanIn(typeof(AspNetCoreMigrationsExtensions).Assembly)
                                     .For.Migrations();
        }
    }
}
