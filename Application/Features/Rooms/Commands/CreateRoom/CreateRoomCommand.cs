using Application.DTOs.ResponseDTOs;
using Domain.Enums;
using MediatR;

namespace Application.Features.Rooms.Commands.CreateRoom
{
    public record CreateRoomCommand(string RoomNumber, RoomStatus Status, Guid RoomTypeId) : IRequest<ResponseDto<bool>>;
    
}
