using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Webapi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddJsonFile("appsettings.json",
                                    optional: false,//不可选
                                    reloadOnChange: false
                                    );
            })
                .UseStartup<Startup>();

        //Create DefaultBuilder
        //public static IWebHostBuilder CreateDefaultBuilder(string[] args) 
        //{
        //    var builder = new WebHostBuilder()
        //        .UseKestrel()
        //        .UseContentRoot(Directory.GetCurrentDirectory())
        //        .ConfigureAppConfiguration((hostingContext,config)=> 
        //        {
        //            var env = hostingContext.HostingEnvironment;
        //            config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        //            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

        //        })
                
                
                
        //}
    }
}
