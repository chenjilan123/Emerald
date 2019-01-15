using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Polaris
{
    public class Program
    {
        /// <summary>
        /// Scoped services are disposed by the container that created them. If a scoped service is created in the root container, the service's lifetime is effectively promoted to singleton because it's only disposed by the root container when app/server is shut down. 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            //http或https均可启动，https为不安全连接
            //using (var host = WebHost.Start("https://localhost:8080", app => app.Response.WriteAsync("Hello World!")))
            //{
            //    Console.WriteLine("Use Ctrl-C to shutdown the host...");
            //    host.WaitForShutdown();
            //}


            //using (var host = WebHost.Start("http://localhost:8080", router => router
            //    .MapGet("hello/{name}", (req, res, data) =>
            //        res.WriteAsync($"Hello, {data.Values["name"]}!"))
            //    .MapGet("buenosdias/{name}", (req, res, data) =>
            //        res.WriteAsync($"Buenos dias, {data.Values["name"]}!"))
            //    .MapGet("throw/{message?}", (req, res, data) =>
            //        throw new Exception((string)data.Values["message"] ?? "Uh oh!")) //Status Code : 500
            //    .MapGet("{greeting}/{name}", (req, res, data) =>
            //        res.WriteAsync($"{data.Values["greeting"]}, {data.Values["name"]}!"))
            //    .MapGet("", (req, res, data) => res.WriteAsync("Hello, World!"))))
            //{
            //    Console.WriteLine("Use Control-C to show down the host......");
            //    host.WaitForShutdown();
            //}

            IApplicationBuilder app;
            IRouteBuilder router1;

            IApplicationLifetime appLifetime = null;
            appLifetime.ApplicationStarted.Register(OnStarted);
            appLifetime.ApplicationStopped.Register(OnStopping);
            appLifetime.ApplicationStopped.Register(OnStopped);

            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                appLifetime.StopApplication();
                // Don't terminate the process immediately, wait for the Main thread to exit gracefully.
                eventArgs.Cancel = true;
            };
        }

        private static void OnStopped()
        {
            throw new NotImplementedException();
        }

        private static void OnStopping()
        {
            throw new NotImplementedException();
        }

        private static void OnStarted()
        {
            throw new NotImplementedException();
        }
    }
}
