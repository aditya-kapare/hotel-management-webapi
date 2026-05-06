using Contracts;
using HMSWebApiProject.Extensions;
using Microsoft.AspNetCore.HttpOverrides;
using NLog;
using Repositories;
using Repositories.DataSeeder;
using System.Text.Json.Serialization;

namespace HMSWebApiProject
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "nlog.config"));
            builder.Services.ConfigureCORS();
            builder.Services.ConfigureIISIntegration();
            builder.Services.ConfigureLoggerService();
            builder.Services.ConfigureRepositoryManager();
            builder.Services.ConfigureServiceManager();
            builder.Services.ConfigureSqlDbContext(builder.Configuration);


            // Add services to the container.


            builder.Services
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(
                        new JsonStringEnumConverter());
                });




            builder.Services.AddAutoMapper(cfg => { cfg.LicenseKey = " "; }, typeof(Program).Assembly);

            //****************************************************************************************************************
            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<RepositoryContext>();
                await SeedRunner.RunAsync(db);
            }
            // Configure the HTTP request pipeline.

            // Configure the HTTP request pipeline.(middelware)
            var logger = app.Services.GetRequiredService<ILoggerManager>();
            app.ConfigureExceptionHandler(logger);
            if (app.Environment.IsProduction())
            {
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });
            app.UseCors("CorsPolicy");


            app.UseAuthorization();

            //app.Run(async context => {
            //    await context.Response.WriteAsync("Hello from Aboli's middleware");
            //});

            app.MapControllers();

            app.Run();
        }
    }
}
