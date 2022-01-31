using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Product.DAL.Context;

namespace Product.Infrastructure.DependencyInjection
{
    public static class DatabaseInstaller
    {
        public static void AddDatabases(this IServiceCollection services)
        {
            services.AddDbContextPool<ProductDbContext>(options => { options.UseMySQL("Server = dev.ozbelsan.com.tr; Port = 3306; Database = ProductDb; Uid = root; Pwd = DevOzbelsan.58; convert zero datetime = true"); });
        }
    }
}
