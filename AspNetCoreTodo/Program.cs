﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreTodo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // CreateWebHostBuilder(args).Build().Run();

            var host = CreateWebHostBuilder(args).Build();
                
                 using (var scope = host.Services.CreateScope())    
             {
                var services = scope.ServiceProvider;

                try
                {
                    SeedData.InitializeAsync(services).Wait();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();

                    logger.LogError(ex, "Error occured seeding database");
                }
            }

                host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}


//using dependency injection to get service collection that SeedData.InitializeAsync() needs and then runs the method to seed the database. If something goes wrong, an error is logged.