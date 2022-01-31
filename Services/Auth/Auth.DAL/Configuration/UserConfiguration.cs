using Auth.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.DAL.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Surname).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Username).HasMaxLength(20).IsRequired();
            builder.Property(p => p.PasswordHash).HasMaxLength(1000).IsRequired();
            builder.Property(p => p.PasswordSalt).HasMaxLength(1000).IsRequired();
            builder.Property(p => p.Email).HasMaxLength(100).IsRequired();
        }
    }
}