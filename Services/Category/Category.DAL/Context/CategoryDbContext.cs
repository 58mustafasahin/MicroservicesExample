using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Category.DAL.Context
{
    public class CategoryDbContext : DbContext, ICategoryDbContext
    {
        public CategoryDbContext(DbContextOptions<CategoryDbContext> options) : base(options) { }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseMySQL("Server = .; Port = 3306; Database = CategoryDb; Uid = root; Pwd = password; convert zero datetime = true");
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Entity.Category> Categories { get; set; }
    }
}