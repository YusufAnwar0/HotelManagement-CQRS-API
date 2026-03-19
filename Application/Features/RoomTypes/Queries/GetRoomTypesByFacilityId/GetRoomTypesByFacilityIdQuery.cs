using Application.DTOs.ResponseDTOs;
using Application.Features.RoomTypes.DTOs;
using MediatR;

namespace Application.Features.RoomTypes.Queries.GetRoomTypesByFacilityId
{
    public record GetRoomTypesByFacilityIdQuery(Guid facilityId) : IRequest<ResponseDto<IEnumerable<GetRoomTypeDTO>>>;
    
}
