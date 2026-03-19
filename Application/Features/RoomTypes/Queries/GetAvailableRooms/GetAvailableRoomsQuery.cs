using Application.DTOs.ResponseDTOs;
using Application.Features.RoomTypes.DTOs;
using MediatR;

namespace Application.Features.RoomTypes.Queries.GetAvailableRooms
{
    public record GetAvailableRoomsQuery(DateTime CheckInDate, DateTime CheckOutDate, int GuestCount) : IRequest<ResponseDto<IEnumerable<RoomTypeAvailabilityDto>>>;
    
}
