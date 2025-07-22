using System;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using StocksApi;
using StocksApi.Data;
using StocksApi.Extensions;

internal class Program
{
    private static async Task Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build())
            .CreateLogger();

        try
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog();
            builder.Services.AddOpenApi();

            var connectionString = builder.Configuration.GetValue<string>("DefaultConnection") 
                ?? Environment.GetEnvironmentVariable("DefaultConnection");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

            // Configure the HTTP request pipeline.
            var app = builder.Build();
            await app.ApplyMigrationsAsync();
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }
            app.UseHttpsRedirection();
            app.UseSerilogRequestLogging();
            app.MapEndpoints();

            await app.RunAsync();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}