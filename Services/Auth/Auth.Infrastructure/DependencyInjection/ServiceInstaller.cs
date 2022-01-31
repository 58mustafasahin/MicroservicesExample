using Auth.Business.Abstract;
using Auth.Business.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Infrastructure.DependencyInjection
{
    public static class ServiceInstaller
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}
