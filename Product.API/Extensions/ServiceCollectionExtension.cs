using Product.Infrastructure.Context;

namespace Product.API.Extensions  
{
    public static class ServiceCollectionExtension 
    {
        public static IServiceCollection AddDatabaseServices(this IServiceCollection services)
        {
            services.AddDbContext<ProductDbContext>();
            return services;
        }
    }
}