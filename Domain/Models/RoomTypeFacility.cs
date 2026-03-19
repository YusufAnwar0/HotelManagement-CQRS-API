namespace Domain.Models
{
    public class RoomTypeFacility : BaseModel
    {
        public Guid RoomTypeId { get; set; }
        public RoomType RoomType { get; set; } 

        public Guid FacilityId { get; set; }
        public Facility Facility { get; set; } 
    }
}