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
            builder.Services.AddSwaggerGen();
            builder.Services.AddDatabaseServices();
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ProductGetAllQuery).Assembly));
            builder.Services.AddMvc();

            builder.Logging.AddConsole();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            } 
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }        

            app.UseHttpsRedirection();
            
            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
            app.MapControllers();

            // TODO Eric : 
            // Afficher dans une console les évènements consommés
            // Solution à déposer sur GitLab ou Github selon votre choix


            //app.UseAuthorization();

            //app.MapGet("/product/{productId}", (int productId) => new ProductModel(productId));

/*
            app.MapGet("/weatherforecast", () =>
            {
                var forecast =  Enumerable.Range(1, 5).Select(index =>
                    new WeatherForecast
                    (
                        DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        Random.Shared.Next(-20, 55),
                        summaries[Random.Shared.Next(summaries.Length)]
                    ))
                    .ToArray();
                return forecast;
            })
            .WithName("GetWeatherForecast")
            .WithOpenApi();
*/
            app.Run();
        }
    }
}



