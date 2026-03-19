using Domain.Enums;

namespace Domain.Models
{
    public class Room : BaseModel
    {
        public string RoomNumber { get; set; }
        public RoomStatus Status { get; set; }

        public Guid RoomTypeId { get; set; }
        public RoomType RoomType { get; set; }

        public ICollection<RoomReservation> RoomReservations { get; set; } = new List<RoomReservation>();


    }
}