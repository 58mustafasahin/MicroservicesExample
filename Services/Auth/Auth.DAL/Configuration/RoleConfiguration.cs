using JWTStructure.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Auth.DAL.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).HasMaxLength(100);

            //builder.HasData(new Role { Id = new Guid("20c4ce04-668f-4d5c-8927-5963c04b0943"), Name = "Admin" });
            //builder.HasData(new Role { Id = new Guid("9d0dbfc5-4e0f-42cd-a282-3352c487e16c"), Name = "User" });
        }
    }
}
