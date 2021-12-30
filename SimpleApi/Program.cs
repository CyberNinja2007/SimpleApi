using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SimpleApi.Data;
using SimpleApi.Models;

namespace SimpleApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
 
                try
                {
                    using (var context = services.GetRequiredService<CitizenContext>())
                    {
                        if (!context.Citizens.Any())
                        {
                            context.Citizens.AddRange(
                                new Citizen { Age = 38, Name = "Demian Goal", Sex = "male" },
                                new Citizen { Age = 23, Name = "Naruto Uzumaki", Sex = "male" },
                                new Citizen { Age = 60, Name = "Peter Parker", Sex = "male" },
                                new Citizen { Age = 86, Name = "Jackie Black", Sex = "female" },
                                new Citizen { Age = 14, Name = "Abby Fir", Sex = "female" });
                        
                            context.SaveChanges();
                        }
                    }
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}