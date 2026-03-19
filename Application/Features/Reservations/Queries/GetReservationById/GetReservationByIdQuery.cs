using Application.DTOs.ResponseDTOs;
using Application.Features.Reservations.DTOs;
using MediatR;

namespace Application.Features.Reservations.Queries.GetReservationById
{
    public record GetReservationByIdQuery(Guid id) : IRequest<ResponseDto<ReservationDetailsDto>>;
    
}
