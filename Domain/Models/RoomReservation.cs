namespace Domain.Models
{
    public class RoomReservation : BaseModel
    {
        public decimal PricePerNight { get; private set; }
        public decimal DiscountAmount { get; private set; }
        public decimal TaxAmount { get; private set; }
        public decimal TotalPrice { get; private set; }


        public Guid RoomId { get; private set; }
        public Room Room { get; private set; }

        public Guid ReservationId { get; set; }
        public Reservation Reservation { get; set; }

        public Guid? OfferId { get; private set; }
        public Offer? Offer { get; private set; }

        public RoomReservation() { }

        public RoomReservation(Guid roomId, decimal basePrice, decimal discountAmount, short numberOfNights, Guid? offerId, decimal taxRate)
        {
            RoomId = roomId;
            PricePerNight = basePrice;
            DiscountAmount = discountAmount;
            OfferId = offerId;

            var roomPriceAfterDiscount = basePrice - discountAmount;
            TaxAmount = roomPriceAfterDiscount * taxRate;
            var finalPricePerNight = roomPriceAfterDiscount + TaxAmount;
            TotalPrice = finalPricePerNight * numberOfNights;

        }
    }
}