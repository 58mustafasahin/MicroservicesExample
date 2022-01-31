using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Product.DAL.Context
{
    public class ProductDbContext : DbContext, IProductDbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseMySQL("Server=dev.ozbelsan.com.tr;Port=3306;Database=ProductDb;Uid=root;Pwd=DevOzbelsan.58;convert zero datetime=true");
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Entity.Product> Products { get; set; }
    }
}