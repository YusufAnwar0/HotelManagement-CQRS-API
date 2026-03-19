using Application.DTOs.ResponseDTOs;
using MediatR;

namespace Application.Features.Rooms.Commands.DeleteRoom
{
    public record DeleteRoomCommand(Guid roomId) : IRequest<ResponseDto<bool>>;
    
}
