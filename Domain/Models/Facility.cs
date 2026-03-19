namespace Domain.Models
{
    public class Facility : BaseModel
    {
        public string Name { get; set; }
        private string _code { get; set; }
        public string Code
        {
            get => _code;
            set => _code = value?.ToUpperInvariant().Trim();
        }
        public string Description { get; set; }
        public ICollection<RoomTypeFacility> RoomTypeFacilities { get; set; } = new List<RoomTypeFacility>();

    }
}