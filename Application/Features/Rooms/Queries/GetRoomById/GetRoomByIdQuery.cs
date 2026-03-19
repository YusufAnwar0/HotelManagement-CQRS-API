using Application.DTOs.ResponseDTOs;
using Application.Features.Rooms.DTOs;
using MediatR;

namespace Application.Features.Rooms.Queries.GetRoomById
{
    public record GetRoomByIdQuery(Guid roomId) : IRequest<ResponseDto<RoomDto>>;
    
}
