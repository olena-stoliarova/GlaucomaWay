using System;
using GlaucomaWay.Repositories;
using GlaucomaWay.Users;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace GlaucomaWay;

public class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Debug()
            .CreateLogger();

        try
        {
            Log.Information("Starting up");
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                try
                {
                    var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
                    var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
                    DataSeeder.SeedData(userManager, roleManager);
                }
                catch (Exception ex)
                {
                    Log.Fatal(ex, ex.Message);
                }
            }

            host.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application start-up failed");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            }).UseSerilog();
}