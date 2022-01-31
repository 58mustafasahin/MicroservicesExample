using Category.Business.Abstract;
using Category.Business.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace Category.Infrastructure.DependencyInjection
{
    public static class ServiceInstaller
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
        }
    }
}
