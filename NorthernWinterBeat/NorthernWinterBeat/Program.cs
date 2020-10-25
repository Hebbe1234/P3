using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NorthernWinterBeatLibrary.Managers;

namespace NorthernWinterBeat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            FestivalManager festivalManager = new FestivalManager();
            CreateHostBuilder(args).Build().Run();


        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureAppConfiguration(builder =>
                {
                    builder.AddJsonFile("appSecret.json");
                });
    }
}
