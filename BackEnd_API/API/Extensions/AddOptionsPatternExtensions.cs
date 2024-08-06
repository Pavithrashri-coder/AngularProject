using Options;
namespace API
{
    public static class AddOptionsPatternExtensions
    {
        public static IServiceCollection AddOptionsPattern(this IServiceCollection services, IConfiguration Configuration)
        {
            services.Configure<ConnectionStringsOptions>(Configuration.GetSection(ConnectionStringsOptions.ConnectionStrings));

            return services;
        }

    }

}
