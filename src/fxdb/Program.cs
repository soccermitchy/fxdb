using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace fxdb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var dir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            IConfigurationBuilder builder = new ConfigurationBuilder();
            Console.WriteLine("loading config");
            if (File.Exists("/etc/webapps/fxdb.json"))
            {
                Console.WriteLine("Using /etc/webapps/fxdb.json");
                builder = builder.AddJsonFile("/etc/webapps/fxdb.json");
            }
            else if (File.Exists(dir + "/appsettings.json"))
            {
                Console.WriteLine("Using appsettings.json");
                builder = builder.AddJsonFile(dir + "/appsettings.json");
            }

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
