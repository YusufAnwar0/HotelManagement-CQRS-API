using Application.DTOs.ResponseDTOs;
using Application.Features.RoomTypes.DTOs;
using MediatR;

namespace Application.Features.RoomTypes.Queries.GetAllRoomTypes
{
    public record GetAllRoomTypesQuery() : IRequest<ResponseDto<IEnumerable<GetRoomTypeDTO>>>;
    
}
