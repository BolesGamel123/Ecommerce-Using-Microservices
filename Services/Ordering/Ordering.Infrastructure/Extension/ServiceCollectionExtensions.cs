using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Core.Repositories;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Repositories;


namespace Ordering.Infrastructure.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            //  DbContext 
            services.AddDbContext<OrderContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("OrderConnectionString"),
                    sqlOptions => sqlOptions.EnableRetryOnFailure()
                ));

            // Repositories
            services.AddScoped(typeof(IAsyncRepository<>), typeof(AsyncRepository<>));
            services.AddScoped<IOrderingRepository, OrderingRepository>();
            return services;
        }
    }
}
