using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Category.DAL.Context
{
    public interface ICategoryDbContext
    {
        DbSet<Entity.Category> Categories { get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
