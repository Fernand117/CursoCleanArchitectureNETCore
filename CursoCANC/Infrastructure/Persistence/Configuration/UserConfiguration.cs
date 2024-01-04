using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id).HasConversion(
            userId => userId.Id,
            value => new UserId(value));

        builder.Property(u => u.UserName).HasMaxLength(50);
        builder.Property(u => u.Email).HasMaxLength(50);
        builder.Property(u => u.Password).HasMaxLength(50);

        builder.HasIndex(u => u.Email).IsUnique();

        builder.Property(u => u.Active).IsRequired(true);
    }
}