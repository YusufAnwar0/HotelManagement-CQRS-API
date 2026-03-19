using Application.DTOs.ResponseDTOs;
using Domain.Enums;
using MediatR;

namespace Application.Features.Rooms.Commands.UpdateRoom
{
    public record UpdateRoomCommand(Guid id, string? RoomNumber, RoomStatus? Status, Guid? RoomTypeId) : IRequest<ResponseDto<bool>>;
   
}
