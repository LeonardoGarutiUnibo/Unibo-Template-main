using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;  // per CreateScope, GetRequiredService
using Microsoft.EntityFrameworkCore;            // per Database.Migrate()
using System;                                   // per Exception, Console
using Template.Services;                            // esempio, sostituisci con il namespace corretto di TemplateDbContext
using Template.Infrastructure;              // per Migrate

namespace Template.Web
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
                    var context = services.GetRequiredService<TemplateDbContext>();
                    if (context.Database.IsRelational())
                    {
                        context.Database.Migrate();
                    }

                    DataGenerator.InitializeUsers(context);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Errore durante la migration o seeding: {ex.Message}");
                    throw;
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(kestrel =>
                    {
                        kestrel.AddServerHeader = false; // OWASP: Remove Kestrel response header 
                    });

                    webBuilder.UseStartup<Startup>();
                });
    }
}
