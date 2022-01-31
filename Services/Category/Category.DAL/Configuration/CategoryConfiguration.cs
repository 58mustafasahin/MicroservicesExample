using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Category.DAL.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Entity.Category>
    {
        public void Configure(EntityTypeBuilder<Entity.Category> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
        }
    }
}
