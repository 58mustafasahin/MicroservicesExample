using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Product.DAL.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Entity.Product>
    {
        public void Configure(EntityTypeBuilder<Entity.Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Price).IsRequired();
        }
    }
}
