using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Polaris
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //无法运行
            //var webHost = new WebHostBuilder()
            //    .UseKestrel()
            //    .UseContentRoot(Directory.GetCurrentDirectory())
            //    .ConfigureAppConfiguration((hostingContext, config) =>
            //    {
            //        var env = hostingContext.HostingEnvironment;
            //        config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            //              .AddJsonFile($"appsettings.{env.EnvironmentName}.json",
            //                  optional: true, reloadOnChange: true);
            //        config.AddEnvironmentVariables();
            //    })
            //    .ConfigureLogging((hostingContext, logging) =>
            //    {
            //        var section = hostingContext.Configuration.GetSection("Logging");
            //        logging.AddConfiguration(section);
            //        logging.AddConsole();
            //        logging.AddDebug();
            //        logging.AddEventSourceLogger();
            //    })
            //    .UseStartup<Startup>()
            //    .Build();
            //webHost.Run();


            var host = CreateWebHostBuilder(args).Build();
            var logger = host.Services.GetRequiredService<ILogger<Program>>();
            //Order 2
            logger.LogInformation("Application running");

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                            .AddJsonFile($"appsettings.{env.EnvironmentName}.json",
                                optional: true, reloadOnChange: true);
                    config.AddEnvironmentVariables();
                })
                .UseStartup<Startup>()
                .ConfigureLogging((hostingContext, logging) =>
                {
                    //Clear default providers
                    logging.ClearProviders();
                    //ILoggingProvider
                    var section = hostingContext.Configuration.GetSection("Logging");
                    logging.AddConfiguration(section);
                    logging.AddConsole();
                    logging.AddDebug();
                    //ASP.NET Core 2.2
                    logging.AddEventSourceLogger();
                });
    }
}
