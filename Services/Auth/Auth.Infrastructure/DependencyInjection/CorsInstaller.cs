using Microsoft.Extensions.DependencyInjection;

namespace Auth.Infrastructure.DependencyInjection
{
    public static class CorsInstaller
    {
        public static void AddCorsOption(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
        }
    }
}
