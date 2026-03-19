namespace Domain.Models
{
    public class RoomTypeOffer : BaseModel
    {
        public Guid RoomTypeId { get; set; }
        public RoomType RoomType { get; set; }
        public Guid OfferId { get; set; }
        public Offer Offer { get; set; }
    }
}