using Application.DTOs.ResponseDTOs;
using MediatR;

namespace Application.Features.Reservations.Commands.CheckOutReservation
{
    public record CheckOutReservationCommand(Guid reservationId) : IRequest<ResponseDto<bool>>;
    
}
