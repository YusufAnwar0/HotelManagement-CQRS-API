using Application.DTOs.ResponseDTOs;
using MediatR;

namespace Application.Features.RoomTypes.Commands.AddFacilitiesToRoomType
{
    public record AddFacilitiesToRoomTypeCommand(Guid roomTypeId, IEnumerable<Guid> facilityIds) : IRequest<ResponseDto<bool>>;
    
}
