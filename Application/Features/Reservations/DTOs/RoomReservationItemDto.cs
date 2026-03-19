namespace Application.Features.Reservations.DTOs
{
    public class RoomReservationItemDto
    {
        public Guid RoomId { get; set; }

        public string RoomNumber { get; set; }

        public decimal PricePerNight { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid? OfferId { get; set; }

    }
}
