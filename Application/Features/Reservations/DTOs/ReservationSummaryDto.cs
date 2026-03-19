using Domain.Enums;

namespace Application.Features.Reservations.DTOs
{
    public class ReservationSummaryDto
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string Status { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
