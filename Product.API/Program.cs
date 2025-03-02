using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using Product.API.ExceptionHandler;
using Product.API.Extensions;
using Product.Application.Queries;

namespace Product.API 
{
    public class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(
                c => { 
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ABCProduct API", Version = "v1" });
                });
            builder.Services.AddDatabaseServices();
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ProductGetAllQuery).Assembly));
            builder.Services.AddMvc();
            builder.Services.AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
            
            builder.Logging.AddConsole();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            } 
            
            app.UseHttpsRedirection();
            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
            app.MapControllers();

            app.Run();
        }
    }
}



