using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class RoomTypeConfiguration : IEntityTypeConfiguration<RoomType>
    {
        public void Configure(EntityTypeBuilder<RoomType> builder)
        {

            builder.HasKey(rt => rt.Id);

            builder.Property(rt => rt.Name).IsRequired().HasMaxLength(50);
            builder.Property(rt => rt.Description).IsRequired().HasMaxLength(500);
            builder.Property(rt => rt.BasePrice).IsRequired();
            builder.Property(rt => rt.MaxCapacity).IsRequired();

            builder.HasMany(rt => rt.Rooms)
                .WithOne(r => r.RoomType)
                .HasForeignKey(r => r.RoomTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(rt => rt.RoomTypeFacilities)
                .WithOne(rtf => rtf.RoomType)
                .HasForeignKey(rtf => rtf.RoomTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(rt => rt.RoomTypeOffers)
                .WithOne(rto => rto.RoomType)
                .HasForeignKey(rto => rto.RoomTypeId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
