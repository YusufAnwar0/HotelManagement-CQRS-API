namespace Application.Features.RoomTypes.DTOs
{
    public class GetRoomTypeDTO
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal BasePrice { get; set; }
        public int MaxCapacity { get; set; }
        public IEnumerable<string> FacilityNames { get; set; }
    }
}
