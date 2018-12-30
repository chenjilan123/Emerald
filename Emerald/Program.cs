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
            CreateWebHostBuilder(args).Build().Run();
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
