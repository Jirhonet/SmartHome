using SmartHome.Repositories;
using SmartHome.Services;

namespace SmartHome
{
    public static class AppExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<DbContext>();
            services.AddScoped<LightRepository>();

            // Services
            services.AddScoped<LightService>();

            return services;
        }
    }
}
