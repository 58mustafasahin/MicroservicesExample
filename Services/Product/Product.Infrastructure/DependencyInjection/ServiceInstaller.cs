using Microsoft.Extensions.DependencyInjection;
using Product.Business.Abstract;
using Product.Business.Concrete;

namespace Product.Infrastructure.DependencyInjection
{
    public static class ServiceInstaller
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
        }
    }
}
