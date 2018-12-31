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

namespace Emerald
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateWebHostBuilder(args).Build().Run();

            //Service Provider
            //The root service provider is created when BuildServiceProvider is called. The root service provider's lifetime corresponds to the app/server's lifetime when the provider starts with the app and is disposed when the app shuts down.
            //Singleton: 
            //Scoped services are disposed by the container that created them. If a scoped service is created in the root container, the service's lifetime is effectively promoted to singleton because it's only disposed by the root container when app/server is shut down.
            var host = CreateWebHostBuilder(args).Build();
            //Call Services:  (IServiceScopeFactory.CreateScope?)
            //Create an IServiceScope with IServiceScopeFactory.CreateScope to resolve a scoped service within the app's scope. This approach is useful to access a scoped service at startup to run initialization tasks. 
            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                try
                {
                    ///var serviceContext = services.GetRequiredService<MyScopedService>();
                    ///Use 

                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred");
                }
            }
            host.Run();
        }

        /// <summary>
        /// ***************External Assembly***************
        /// An IHostingStartup implementation allows adding enhancements to an app at startup from an external assembly outside of the app's Startup class. For more information, see Enhance an app from an external assembly.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    //When a query string parameter for option is provided, the middleware processes the value assignment before the MVC middleware renders the response:
                    services.AddTransient<IStartupFilter, RequestSetOptionsStartupFilter>();
                })
                .UseStartup<Startup>();
    }
}
