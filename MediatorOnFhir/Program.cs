using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;


namespace MediatorOnFhir
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args)
                .Build()
                .Run();
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    IHostingEnvironment environment = context.HostingEnvironment;
                    config
                        .SetBasePath(environment.ContentRootPath)
                        .AddJsonFile("appsettings.json", optional: false)
                        .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true)
                        .AddJsonFile("appsettings.localdev.json", optional: true)
                        .AddEnvironmentVariables();
                })
                .UseStartup<Startup>();
    }
}