using System;
using System.IO;

using Apollo.Migrations;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Serilog;
using Serilog.Events;

namespace Apollo.Master
{
    public class Program
    {
        private static string Env => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? EnvironmentName.Production;

        public static void Main(string[] args)
        {
            var config = CreateConfiguration();

            ConfigureLogger(config);

            try
            {
                Log.Information("Starting application...");

                var host = BuildWebHost(config);

                using (var scope = host.Services.CreateScope())
                {
                    scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>()
                         .Initialize();
                }

                host.Run();
            }
            catch (Exception e)
            {
                Log.Fatal(e, "Application terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static IWebHost BuildWebHost(IConfiguration config)
        {
            return new WebHostBuilder()
                   .UseContentRoot(Directory.GetCurrentDirectory())
                   .UseKestrel()
                   .UseStartup<Startup>()
                   .UseConfiguration(config)
                   .UseSerilog()
                   .Build();
        }

        private static IConfiguration CreateConfiguration()
        {
            return new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddYamlFile($"appsettings.yml", false, false)
                   .AddYamlFile($"appsettings.{Env}.yml", true, false)
                   .Build();
        }

        private static void ConfigureLogger(IConfiguration config)
        {
            Log.Logger = new LoggerConfiguration()
                         .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                         .MinimumLevel.Override("System", LogEventLevel.Warning)
                         .Enrich.FromLogContext()
                         .WriteTo.Console()
                         .WriteTo.RollingFile("logs/events-{Date}.log")
                         .ReadFrom.Configuration(config)
                         .CreateLogger();
        }
    }
}
