using Application.DTOs.ResponseDTOs;
using MediatR;

namespace Application.Features.Reservations.Commands.DeleteReservation
{
    public record DeleteReservationCommand(Guid reservationId) : IRequest<ResponseDto<bool>>;
    
}
