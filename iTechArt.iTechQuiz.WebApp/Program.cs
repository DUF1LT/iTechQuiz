using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;

namespace iTechArt.iTechQuiz.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string outputTemplate = "{Timestamp:yyyy-MM-ddHH: mm: ss.fff}[{Level}] {Message}{NewLine}{Exception}";
            Log.Logger = new LoggerConfiguration().WriteTo.File("log.txt",
            rollingInterval: RollingInterval.Day, 
            outputTemplate: outputTemplate).CreateLogger();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseSerilog();
    }
}
