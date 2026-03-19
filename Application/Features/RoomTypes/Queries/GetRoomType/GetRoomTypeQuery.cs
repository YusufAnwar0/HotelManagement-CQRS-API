using Application.DTOs.ResponseDTOs;
using Application.Features.RoomTypes.DTOs;
using MediatR;

namespace Application.Features.RoomTypes.Queries.GetRoomType
{
    public record GetRoomTypeQuery(Guid roomTypeId) : IRequest<ResponseDto<GetRoomTypeDTO>>;
    
}
