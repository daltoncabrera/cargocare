using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace MSESG.CargoCare.Web
{
    public class Program
    {
        public static IConfiguration Configuration { get; set; }
        private static int _port = 8090;
        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            // Read port from environment variable first, then from configuration
            var portFromEnv = Environment.GetEnvironmentVariable("PORT");
            if (!string.IsNullOrWhiteSpace(portFromEnv) && int.TryParse(portFromEnv, out int envPort))
            {
                _port = envPort;
            }
            else if (!string.IsNullOrWhiteSpace(Configuration["port"]))
            {
                _port = Convert.ToInt32(Configuration["port"]);
            }

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args).UseKestrel(o =>
                {
                    o.Listen(IPAddress.Loopback, _port);
                })
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseSetting("detailedErrors", "true")
                .UseIISIntegration()
                .UseDefaultServiceProvider(options => options.ValidateScopes = false)
                .UseStartup<Startup>()
                .CaptureStartupErrors(true)
                // .UseApplicationInsights()
                .Build();

    }
}

