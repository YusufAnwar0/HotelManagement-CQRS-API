using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class FacilityConfiguration : IEntityTypeConfiguration<Facility>
    {
        public void Configure(EntityTypeBuilder<Facility> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Name).IsRequired().HasMaxLength(50);
            builder.Property(f => f.Code).IsRequired().HasMaxLength(50);
            builder.Property(f => f.Description).IsRequired().HasMaxLength(500);

            builder.HasMany(f => f.RoomTypeFacilities)
                .WithOne(rtf => rtf.Facility)
                .HasForeignKey(rtf => rtf.FacilityId)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
