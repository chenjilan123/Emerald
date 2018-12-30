using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald
{
    public class Program1
    {
        public static IHostingEnvironment HostingEnvironment { get; set; }
        public static IConfiguration Configuration { get; set; }

        /// <summary>
        /// Without Startup file.
        /// ConfigureServices and Configure convenience methods can be used instead of specifying a Startup class. Multiple calls to ConfigureServices append to one another. Multiple calls to Configure use the last method call.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    HostingEnvironment = hostingContext.HostingEnvironment;
                    Configuration = config.Build();
                })
                .ConfigureServices(services =>
                {
                    services.AddMvc();
                })
                .Configure(app =>
                {
                    var loggerFactory = app.ApplicationServices
                        .GetRequiredService<ILoggerFactory>();
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogInformation("Logged in Configure");

                    if (HostingEnvironment.IsDevelopment())
                    {
                        app.UseDeveloperExceptionPage();
                    }
                    else
                    {
                        app.UseExceptionHandler("/Error");
                    }

                    // Configuration is available during startup. Examples:
                    // Configuration["key"]
                    // Configuration["subsection:suboption1"]

                    app.UseMvcWithDefaultRoute();
                    app.UseStaticFiles();
                }).Build().Run();
    }
}
