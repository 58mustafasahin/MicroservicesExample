using Auth.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Infrastructure.DependencyInjection
{
    public static class DatabaseInstaller
    {
        public static void AddDatabases(this IServiceCollection services)
        {
            services.AddDbContextPool<IAuthDbContext,AuthDbContext>(options => { options.UseMySQL("Server = .; Port = 3306; Database = AuthDb; Uid = root; Pwd = password; convert zero datetime = true"); });
        }
    }
}
