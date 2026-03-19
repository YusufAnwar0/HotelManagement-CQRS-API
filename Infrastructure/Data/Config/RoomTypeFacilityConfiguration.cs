using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class RoomTypeFacilityConfiguration : IEntityTypeConfiguration<RoomTypeFacility>
    {
        public void Configure(EntityTypeBuilder<RoomTypeFacility> builder)
        {
            builder.HasKey(rtf => rtf.Id);

            builder.HasIndex(rtf => new { rtf.RoomTypeId, rtf.FacilityId }).IsUnique();

        }
    }
}
