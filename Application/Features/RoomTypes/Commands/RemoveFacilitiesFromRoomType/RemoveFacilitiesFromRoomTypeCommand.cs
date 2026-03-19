using Application.DTOs.ResponseDTOs;
using MediatR;

namespace Application.Features.RoomTypes.Commands.RemoveFacilitiesFromRoomType
{
    public record RemoveFacilitiesFromRoomTypeCommand(Guid roomTypeId, IEnumerable<Guid> facilityIds) : IRequest<ResponseDto<bool>>;
    
}
