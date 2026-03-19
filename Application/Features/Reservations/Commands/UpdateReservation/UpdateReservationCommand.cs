using Application.DTOs.ResponseDTOs;
using Application.Features.Reservations.DTOs;
using MediatR;

namespace Application.Features.Reservations.Commands.UpdateReservation
{
    public record UpdateReservationCommand(Guid id, DateTime CheckInDate, DateTime CheckOutDate, IEnumerable<RoomTypeBookingItem> RoomTypesToBook) : IRequest<ResponseDto<bool>>;
}
