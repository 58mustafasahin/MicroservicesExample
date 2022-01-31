using Category.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Category.Infrastructure.DependencyInjection
{
    public static class DatabaseInstaller
    {
        public static void AddDatabases(this IServiceCollection services)
        {
            services.AddDbContextPool<CategoryDbContext>(options => { options.UseMySQL("Server = .; Port = 3306; Database = CategoryDb; Uid = root; Pwd = password; convert zero datetime = true"); });
        }
    }
}
