using Application.DTOs.ResponseDTOs;
using Application.Features.RoomTypes.Commands.AddFacilitiesToRoomType;
using Application.Features.RoomTypes.Commands.CreateRoomType;
using Application.Features.RoomTypes.Commands.DeleteRoomType;
using Application.Features.RoomTypes.Commands.RemoveFacilitiesFromRoomType;
using Application.Features.RoomTypes.Commands.UpdateRoomType;
using Application.Features.RoomTypes.DTOs;
using Application.Features.RoomTypes.Queries.GetAllRoomTypes;
using Application.Features.RoomTypes.Queries.GetAvailableRooms;
using Application.Features.RoomTypes.Queries.GetRoomType;
using Application.Features.RoomTypes.Queries.GetRoomTypesByFacilityId;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomTypesController : ControllerBase
    {
        private readonly ISender _sender;

        public RoomTypesController(ISender sender)
        {
            _sender = sender;
        }


        [HasPermission(Permissions.GetAllRoomTypes)]
        [HttpGet]
        public async Task<ResponseDto<IEnumerable<GetRoomTypeDTO>>> GetAll()
        {
            var query = new GetAllRoomTypesQuery();
            var response = await _sender.Send(query);
            return response;
        }

        [HasPermission(Permissions.GetRoomTypeById)]
        [HttpGet("{id:guid}")]
        public async Task<ResponseDto<GetRoomTypeDTO>> GetById(Guid id)
        {
            var query = new GetRoomTypeQuery(id);
            return await _sender.Send(query);
        }

        [HasPermission(Permissions.CreateRoomType)]
        [HttpPost]
        public async Task<ResponseDto<bool>> Create([FromBody] CreateRoomTypeCommand command)
        {
            return await _sender.Send(command);
        }

        [HasPermission(Permissions.UpdateRoomType)]
        [HttpPut("{id:guid}")]
        public async Task<ResponseDto<bool>> Update(Guid id, [FromBody] UpdateRoomTypeCommand command)
        {
            if (id != command.Id)
            {
                return ResponseDto<bool>.Fail(ErrorCode.ValidationError, "ID mismatch");
            }

            return await _sender.Send(command);
        }

        [HasPermission(Permissions.DeleteRoomType)]
        [HttpDelete("{id:guid}")]
        public async Task<ResponseDto<bool>> Delete(Guid id)
        {
            var command = new DeleteRoomTypeCommand(id);
            return await _sender.Send(command);
        }


        [HasPermission(Permissions.AddFacilitiesToRoomType)]
        [HttpPost("{id:guid}/facilities")]
        public async Task<ResponseDto<bool>> AddFacilities(Guid id, [FromBody] AddFacilitiesToRoomTypeCommand command)
        {
            if (id != command.roomTypeId)
            {
                return ResponseDto<bool>.Fail(ErrorCode.ValidationError, "The RoomType ID in the URL does not match the command body.");
            }

            return await _sender.Send(command);
        }

        [HasPermission(Permissions.RemoveFacilitiesFromRoomType)]
        [HttpDelete("{id:guid}/facilities")]
        public async Task<ResponseDto<bool>> RemoveFacilities(Guid id, [FromBody] RemoveFacilitiesFromRoomTypeCommand command)
        {
            if (id != command.roomTypeId)
            {
                return ResponseDto<bool>.Fail(ErrorCode.ValidationError, "The RoomType ID in the URL does not match the command body.");
            }

            return await _sender.Send(command);
        }

        [HasPermission(Permissions.GetRoomTypesByFacility)]
        [HttpGet("facility/{facilityId:guid}")]
        public async Task<ResponseDto<IEnumerable<GetRoomTypeDTO>>> GetByFacility(Guid facilityId)
        {
            var query = new GetRoomTypesByFacilityIdQuery(facilityId);
            return await _sender.Send(query);
        }

        [HasPermission(Permissions.GetAvailableRoomTypes)]
        [HttpGet("available")]
        public async Task<ResponseDto<IEnumerable<RoomTypeAvailabilityDto>>> GetAvailableRoomTypes([FromQuery] DateTime checkIn, [FromQuery] DateTime checkOut, [FromQuery] int guestsCount)
        {
            var query = new GetAvailableRoomsQuery(checkIn, checkOut, guestsCount);
            var response = await _sender.Send(query);

            return response;
        }

    }
}

