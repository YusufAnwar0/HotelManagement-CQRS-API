using Application.DTOs.ResponseDTOs;
using Application.Features.Reservations.Commands.CancelReservation;
using Application.Features.Reservations.Commands.CheckInReservation;
using Application.Features.Reservations.Commands.CheckOutReservation;
using Application.Features.Reservations.Commands.CreateReservation;
using Application.Features.Reservations.Commands.DeleteReservation;
using Application.Features.Reservations.Commands.UpdateReservation;
using Application.Features.Reservations.DTOs;
using Application.Features.Reservations.Queries.GetReservationById;
using Application.Features.Reservations.Queries.GetUserReservations;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly ISender _sender;

        public ReservationsController(ISender sender)
        {
            _sender = sender;
        }

        [HasPermission(Permissions.CreateReservation)]
        [HttpPost]
        public async Task<ResponseDto<bool>> Create([FromBody] CreateReservationCommand command)
        {
            return await _sender.Send(command);
        }

        [HasPermission(Permissions.UpdateReservation)]
        [HttpPut("{id:guid}")]
        public async Task<ResponseDto<bool>> Update(Guid id, [FromBody] UpdateReservationCommand command)
        {
            if (id != command.id)
            {
                return ResponseDto<bool>.Fail(Domain.Enums.ErrorCode.ValidationError, "ID mismatch");
            }
            return await _sender.Send(command);
        }

        [HasPermission(Permissions.DeleteReservation)]
        [HttpDelete("{id:guid}/delete")]
        public async Task<ResponseDto<bool>> Delete(Guid id)
        {
            return await _sender.Send(new DeleteReservationCommand(id));
        }

        [HasPermission(Permissions.CancelReservation)]
        [HttpPut("{id:guid}/cancel")]
        public async Task<ResponseDto<bool>> Cancel(Guid id)
        {
            return await _sender.Send(new CancelReservationCommand(id));
        }

        [HasPermission(Permissions.CheckInReservation)]
        [HttpPut("{id:guid}/check-in")]
        public async Task<ResponseDto<bool>> CheckIn(Guid id)
        {
            return await _sender.Send(new CheckInReservationCommand(id));
        }

        [HasPermission(Permissions.CheckOutReservation)]
        [HttpPut("{id:guid}/check-out")]
        public async Task<ResponseDto<bool>> CheckOut(Guid id)
        {
            return await _sender.Send(new CheckOutReservationCommand(id));
        }

        [HasPermission(Permissions.GetReservationById)]
        [HttpGet("{id:guid}")]
        public async Task<ResponseDto<ReservationDetailsDto>> GetById(Guid id)
        {
            return await _sender.Send(new GetReservationByIdQuery(id));
        }

        [HasPermission(Permissions.GetUserReservations)]
        [HttpGet("user/{userId:guid}/history")]
        public async Task<ResponseDto<IEnumerable<ReservationSummaryDto>>> GetUserReservations(Guid userId, [FromQuery] int pageNumber = 1)
        {
            return await _sender.Send(new GetUserReservationsQuery(userId, pageNumber));
        }

    }
}
