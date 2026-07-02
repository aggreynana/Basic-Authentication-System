using BasicAuth.Entities;
using Microsoft.EntityFrameworkCore;

namespace BasicAuth.Configs;

public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(u => u.LastName).IsRequired().HasMaxLength(50);
    }
}