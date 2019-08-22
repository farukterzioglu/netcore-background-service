using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {   
            var builder = new HostBuilder()
                .ConfigureHostConfiguration(configHost =>
                {
                    configHost.SetBasePath(Directory.GetCurrentDirectory());
                    configHost.AddJsonFile("hostsettings.json", optional: true);
                    configHost.AddEnvironmentVariables(prefix: "CONSOLEAPP_");
                })
                .ConfigureAppConfiguration((hostContext, configApp) =>
                {
                    configApp.SetBasePath(Directory.GetCurrentDirectory());
                    configApp.AddJsonFile("appsettings.json", optional: false);
                    configApp.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true);
                    configApp.AddEnvironmentVariables();
                })
                .UseSerilog((hostingContext, loggerConfiguration) =>
                {
                    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
                })
                .ConfigureServices((hostContext, services) => {
                    services.Configure<ApplicationSettings>(hostContext.Configuration);
                    services.Configure<AuthenticationSettings>(hostContext.Configuration.GetSection("authentication"));
                    services.Configure<ExternalSettings>( (options) => { 
                        // TODO: Get values from external settings provider (eg. Consul)
                        options.SomeValue = 1000;
                        options.AnotherValue = "a value";
                    });
                    services.Configure<ServerSettings>(hostContext.Configuration);
                     
                    AuthenticationSettings bindedSettings = new AuthenticationSettings();
                    hostContext.Configuration.Bind("authentication", bindedSettings);
                    services.AddSingleton<AuthenticationSettings>(bindedSettings);

                    services.AddTransient<IRepository, Repository>();
                    services.AddHostedService<ApplicationService>();
                    services.AddHostedService<AnotherService>();
                    services.AddHostedService<AnotherService>();

                    services.AddSingleton<ConcurrentQueue<string>>((p) => { return new ConcurrentQueue<string>(); });
                    services.AddHostedService<ProducerService>();
                    services.AddHostedService<ConsumerService>();
                    services.AddHostedService<ConsumerService>();
                });
                
            await builder.RunConsoleAsync();
        }
    }
}
