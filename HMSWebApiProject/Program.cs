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



            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(
                        new System.Text.Json.Serialization.JsonStringEnumConverter());
                });


            builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
            {
                options.SerializerOptions.Converters.Add(
                    new System.Text.Json.Serialization.JsonStringEnumConverter());
            });






            builder.Services.AddAutoMapper(cfg => { cfg.LicenseKey = " "; }, typeof(Program).Assembly);

            //****************************************************************************************************************
            var app = builder.Build();

            //using (var scope = app.Services.CreateScope())
            //{
            //    var db = scope.ServiceProvider.GetRequiredService<RepositoryContext>();
            //    await SeedRunner.RunAsync(db);
            //}
            

    
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

        
            app.MapControllers();

            app.Run();
        }
    }
}
