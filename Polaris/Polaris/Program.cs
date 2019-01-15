using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Polaris
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var host = WebHost.Start("http://localhost:8080", app => app.Response.WriteAsync("Hello World!")))
            {
                Console.WriteLine("Use Ctrl-C to shutdown the host...");
                host.WaitForShutdown();
            }
        }
    }
}
