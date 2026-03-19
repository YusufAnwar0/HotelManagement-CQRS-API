namespace Application.Features.Offers.DTOs
{
    public class GetOfferDto
    {
        public Guid id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal DiscountValue { get; set; }
    }
}
