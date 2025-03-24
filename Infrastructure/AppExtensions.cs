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
            services.AddScoped<SpeakerRepository>();

            // Services
            services.AddScoped<LightService>();
            services.AddScoped<SpeakerService>();

            return services;
        }
    }
}
