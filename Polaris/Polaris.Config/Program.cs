using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Polaris.Config.Extensions;

namespace Polaris.Config
{
    public class Program
    {
        public static Dictionary<string, string> arrayDict = new Dictionary<string, string>
        {
            {"watchsunshine0", "fantastic0"},
            {"watchsunshine1", "fantastic1"},
            {"watchsunshine2", "fantastic2"},
            {"watchsunshine3", "fantastic3"},
            {"watchsunshine4", "fantastic4"},
        };
        public static readonly Dictionary<string, string> _switchMappings =
         new Dictionary<string, string>
         {
            { "-CLKey1", "CommandLineKey1" },
            { "-CLKey2", "CommandLineKey2" }
         };
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    //应用程序是在IIS Express根目录运行，而非项目根目录。
                    // C:\\Program Files\\IIS Express
                    var basePath = Directory.GetCurrentDirectory();
                    basePath = Path.Combine(basePath, "config");
                    config.SetBasePath(basePath);
                    //内存配置
                    //The MemoryConfigurationProvider uses an in-memory collection as configuration key - value pairs.
                    config.AddInMemoryCollection(arrayDict);

                    //文件配置(json, xml, ini)
                    config.AddIniFile("Building.ini", optional: true, reloadOnChange: true);
                    //The JSON Configuration Provider is established first. Therefore, user secrets, environment variables, and command-line arguments override configuration set by the appsettings files.
                    config.AddJsonFile("hierarchy.json", optional: true, reloadOnChange: true);
                    config.AddJsonFile("keyvault.json", optional: true, reloadOnChange: true);
                    //Xml: The root node of the configuration file is ignored when the configuration key-value pairs are created. Don't specify a Document Type Definition (DTD) or namespace in the file.
                    config.AddXmlFile("Section.xml", optional: true, reloadOnChange: true);

                    //Key-per-file 配置
                    //The directoryPath to the files must be an absolute path.
                    //The double-underscore (__) is used as a configuration key delimiter in file names. For example, the file name Logging__LogLevel__System produces the configuration key Logging:LogLevel:System.
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "path/to/files");
                    config.AddKeyPerFile(directoryPath: path, optional: true);

                    //EntityFramework Provider
                    config.AddEFConfiguration(options => options.UseInMemoryDatabase("InMemoryDb"));

                    //激活命令行配置
                    //If the arguments include a mapped switch and are passed to CreateDefaultBuilder, its AddCommandLine provider fails to initialize with a FormatException.
                    config.AddCommandLine(args, _switchMappings);
                })
                .UseStartup<Startup>();
    }
}
