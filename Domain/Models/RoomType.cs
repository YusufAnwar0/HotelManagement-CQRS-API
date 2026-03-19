namespace Domain.Models
{
    public class RoomType : BaseModel
    {
        private string _code;
        public string Code 
        {
            get => _code;
            set => _code = value.ToUpperInvariant().Trim();
        }
        public string Name { get; set; } 
        public string Description { get; set; }
        public decimal BasePrice { get; set; }
        public int MaxCapacity { get; set; }


        public ICollection<Room> Rooms { get; set; } = new List<Room>();
        public ICollection<RoomTypeFacility> RoomTypeFacilities { get; set; } = new List<RoomTypeFacility>();
        public ICollection<RoomTypeOffer> RoomTypeOffers { get; set; } = new List<RoomTypeOffer>();

    }
}
