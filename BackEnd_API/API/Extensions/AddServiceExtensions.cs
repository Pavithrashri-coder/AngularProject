using BusinessLayer;
using DataAccessLayer;
using Options;
namespace API
{
    public static class AddServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IDbContext, DbContext>();
            services.AddScoped<IAutoMapperService, AutoMapperService>();
            services.AddScoped<IBrandsService, BrandsService>();
            services.AddScoped<IBrandsRepository, BrandsRepository>();
            return services;
        }
    }
}
