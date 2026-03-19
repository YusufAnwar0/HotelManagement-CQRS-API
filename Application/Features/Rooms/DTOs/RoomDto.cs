using Domain.Enums;

namespace Application.Features.Rooms.DTOs
{
    public class RoomDto
    {
        public string RoomNumber { get; set; }
        public RoomStatus Status { get; set; }
        public string RoomTypeName { get; set; }
        public decimal BasePrice { get; set; }
        public IEnumerable<string> FacilityNames { get; set; }
    }
}
