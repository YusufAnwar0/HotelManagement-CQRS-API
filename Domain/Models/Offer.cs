namespace Domain.Models
{
    public class Offer : BaseModel
    {
        private string _code;
        public string Code 
        {
            get => _code; 
            set => _code = value.ToUpperInvariant().Trim(); 
        }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal DiscountValue { get; set; }

        public ICollection<RoomTypeOffer> RoomTypeOffers { get; set; } = new List<RoomTypeOffer>();

    }
}