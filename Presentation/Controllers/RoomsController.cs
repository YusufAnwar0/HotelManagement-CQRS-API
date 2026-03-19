using Application.DTOs.ResponseDTOs;
using Application.Features.Rooms.Commands.CreateRoom;
using Application.Features.Rooms.Commands.DeleteRoom;
using Application.Features.Rooms.Commands.UpdateRoom;
using Application.Features.Rooms.DTOs;
using Application.Features.Rooms.Queries.GetAllRooms;
using Application.Features.Rooms.Queries.GetRoomById;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly ISender _sender;

        public RoomsController(ISender sender)
        {
            _sender = sender;
        }

        [HasPermission(Permissions.CreateRoom)]
        [HttpPost]
        public async Task<ResponseDto<bool>> Create([FromBody] CreateRoomCommand command)
        {
            return await _sender.Send(command);
        }

        [HasPermission(Permissions.DeleteRoom)]
        [HttpDelete("{roomId:guid}")]
        public async Task<ResponseDto<bool>> Delete(Guid roomId)
        {
            return await _sender.Send(new DeleteRoomCommand(roomId));
        }

        [HasPermission(Permissions.UpdateRoom)]
        [HttpPut("{id:guid}")]
        public async Task<ResponseDto<bool>> Update(Guid id, [FromBody] UpdateRoomCommand command)
        {
            if (id != command.id)
            {
                return ResponseDto<bool>.Fail(Domain.Enums.ErrorCode.ValidationError, "ID mismatch");
            }
            return await _sender.Send(command);
        }

        [HasPermission(Permissions.GetAllRooms)]
        [HttpGet]
        public async Task<ResponseDto<IEnumerable<RoomDto>>> GetAll([FromQuery] int pageNumber = 1)
        {
            return await _sender.Send(new GetAllRoomsQuery(pageNumber));
        }

        [HasPermission(Permissions.GetRoomById)]
        [HttpGet("{roomId:guid}")]
        public async Task<ResponseDto<RoomDto>> GetById(Guid roomId)
        {
            return await _sender.Send(new GetRoomByIdQuery(roomId));
        }
    }
}
