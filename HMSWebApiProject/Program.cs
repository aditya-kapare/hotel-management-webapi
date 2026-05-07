using Contracts;
using HMSWebApiProject.Extensions;
using Microsoft.AspNetCore.HttpOverrides;
using NLog;
using System.Text.Json.Serialization;

namespace HMSWebApiProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "nlog.config"));
            builder.Services.ConfigureCORS();
            builder.Services.ConfigureIISIntegration();
            builder.Services.ConfigureLoggerService();
            builder.Services.ConfigureRepositoryManager();
            builder.Services.ConfigureServiceManager();
            builder.Services.ConfigureSqlDbContext(builder.Configuration);
            builder.Services.AddControllers()
     .AddJsonOptions(options =>
     {
         // ? REMOVE string-enum conversion
         options.JsonSerializerOptions.Converters.Clear();
     });


            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddAutoMapper(cfg => { cfg.LicenseKey = " "; }, typeof(Program).Assembly);

            //****************************************************************************************************************
            var app = builder.Build();

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
        