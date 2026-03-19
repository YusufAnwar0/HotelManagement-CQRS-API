using Application.DTOs.ResponseDTOs;
using Application.Features.Reservations.DTOs;
using MediatR;

namespace Application.Features.Reservations.Commands.CreateReservation
{
    public record CreateReservationCommand (Guid userId ,DateTime CheckInDate, DateTime CheckOutDate, IEnumerable<RoomTypeBookingItem> RoomTypesToBook) : IRequest<ResponseDto<bool>>;
    
}
