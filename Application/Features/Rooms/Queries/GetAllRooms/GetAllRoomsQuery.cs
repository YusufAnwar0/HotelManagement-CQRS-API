using Application.DTOs.ResponseDTOs;
using Application.Features.Rooms.DTOs;
using MediatR;

namespace Application.Features.Rooms.Queries.GetAllRooms
{
    public record GetAllRoomsQuery(int pageNumber = 1) : IRequest<ResponseDto<IEnumerable<RoomDto>>>;
    
}
