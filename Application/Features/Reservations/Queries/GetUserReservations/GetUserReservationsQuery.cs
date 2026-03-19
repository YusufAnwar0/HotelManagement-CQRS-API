using Application.DTOs.ResponseDTOs;
using Application.Features.Reservations.DTOs;
using MediatR;

namespace Application.Features.Reservations.Queries.GetUserReservations
{
    public record GetUserReservationsQuery(Guid userId, int pageNumber = 1, int pageSize = 10) : IRequest<ResponseDto<IEnumerable<ReservationSummaryDto>>>;
    
}
