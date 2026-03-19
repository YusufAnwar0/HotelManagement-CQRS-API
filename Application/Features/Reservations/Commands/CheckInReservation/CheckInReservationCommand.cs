using Application.DTOs.ResponseDTOs;
using MediatR;

namespace Application.Features.Reservations.Commands.CheckInReservation
{
    public record CheckInReservationCommand(Guid reservationId) : IRequest<ResponseDto<bool>>;
    
}
