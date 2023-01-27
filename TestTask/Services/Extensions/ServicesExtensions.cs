using TestTask.Services.Interfaces;

namespace TestTask.Services.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IFeedService, FeedService>();

            return services;
        }
    }
}
