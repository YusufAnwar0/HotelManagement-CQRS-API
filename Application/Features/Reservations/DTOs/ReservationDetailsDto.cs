namespace Application.Features.Reservations.DTOs
{
    public class ReservationDetailsDto
    {
        public Guid id { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public short NumberOfNights { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<RoomReservationItemDto> Rooms { get; set; } = new();
    }
}
