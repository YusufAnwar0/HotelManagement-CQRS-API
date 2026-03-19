using Application.Features.Offers.DTOs;

namespace Application.Features.RoomTypes.DTOs
{
    public class RoomTypeAvailabilityDto
    {
        public Guid id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal BasePrice { get; set; }
        public int MaxCapacity { get; set; }

        public bestOfferDto? bestOffer { get; set; }
    }
}
