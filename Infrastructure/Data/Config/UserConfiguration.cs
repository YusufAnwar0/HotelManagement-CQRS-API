using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Email).IsRequired(false).HasMaxLength(150);
        builder.HasIndex(u => u.Email).IsUnique().HasFilter("[Email] IS NOT NULL");

        builder.Property(u => u.PhoneNumber).IsRequired().HasMaxLength(20);
        builder.HasIndex(u => u.PhoneNumber).IsUnique();

        builder.Property(u => u.UserName).IsRequired().HasMaxLength(100);
        builder.Property(u => u.PasswordHash).IsRequired().HasMaxLength(256);
        builder.Property(u => u.Country).HasMaxLength(100);

        builder.HasMany(u => u.UserRoles)
            .WithOne(ur => ur.User)
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.Reservations)
            .WithOne(r => r.User)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
