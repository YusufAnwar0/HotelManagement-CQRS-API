using Application.DTOs.ResponseDTOs;
using Application.Features.Facilities.Commands.CreateFacility;
using Application.Features.Facilities.Commands.DeleteFacility;
using Application.Features.Facilities.Commands.UpdateFacility;
using Application.Features.Facilities.DTOs;
using Application.Features.Facilities.Queries.GetAllFacilities;
using Application.Features.Facilities.Queries.GetFacilityById;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacilitiesController : ControllerBase
    {
        private readonly ISender _sender;
        public FacilitiesController(ISender sender)
        {
            _sender = sender;
        }

        [HasPermission(Permissions.GetAllFacilities)]
        [HttpGet]
        public async Task<ResponseDto<IEnumerable<GetFacilityDto>>> GetAll()
        {
            return await _sender.Send(new GetAllFacilitiesQuery());
        }

        [HasPermission(Permissions.GetFacilityById)]
        [HttpGet("{id:guid}")]
        public async Task<ResponseDto<GetFacilityDto>> GetById(Guid id)
        {
            return await _sender.Send(new GetFacilityByIdQuery(id));
        }

        [HasPermission(Permissions.CreateFacility)]
        [HttpPost]
        public async Task<ResponseDto<bool>> Create([FromBody] CreateFacilityCommand command)
        {
            return await _sender.Send(command);
        }

        [HasPermission(Permissions.UpdateFacility)]
        [HttpPut("{id:guid}")]
        public async Task<ResponseDto<bool>> Update(Guid id, [FromBody] UpdateFacilityCommand command)
        {
            if (id != command.id)
            {
                return ResponseDto<bool>.Fail(Domain.Enums.ErrorCode.ValidationError, "ID mismatch");
            }
            return await _sender.Send(command);
        }

        [HasPermission(Permissions.DeleteFacility)]
        [HttpDelete("{id:guid}")]
        public async Task<ResponseDto<bool>> Delete(Guid id)
        {
            return await _sender.Send(new DeleteFacilityCommand(id));
        }

    }
}

