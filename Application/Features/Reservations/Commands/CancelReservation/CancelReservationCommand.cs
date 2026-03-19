using Application.DTOs.ResponseDTOs;
using MediatR;

namespace Application.Features.Reservations.Commands.CancelReservation
{
    public record CancelReservationCommand(Guid reservationId) : IRequest<ResponseDto<bool>>;
    
}
