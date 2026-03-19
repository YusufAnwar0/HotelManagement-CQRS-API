namespace Application.Features.Reservations.DTOs
{
    public class RoomTypeBookingItem
    {
        public Guid RoomTypeId { get; set; }
        public int Quantity { get; set; }
        public int GuestsCount { get; set; }
    }
}
