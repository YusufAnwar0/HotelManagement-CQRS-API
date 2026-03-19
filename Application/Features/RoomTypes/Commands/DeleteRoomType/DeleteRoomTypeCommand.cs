using Application.DTOs.ResponseDTOs;
using MediatR;

namespace Application.Features.RoomTypes.Commands.DeleteRoomType
{
    public record DeleteRoomTypeCommand(Guid roomTypeId) : IRequest<ResponseDto<bool>>;
    
}
