using Application.DTOs.ResponseDTOs;
using Application.Features.Roles.Commands.AssignPermissions;
using Application.Features.Roles.Commands.CreateRole;
using Application.Features.Roles.Commands.DeleteRole;
using Application.Features.Roles.Commands.UnassignPermission;
using Application.Features.Roles.DTOs;
using Application.Features.Roles.Queries.GetAllRoles;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HasPermission(Permissions.CreateRole)]
        [HttpPost("Role")]
        public async Task<ResponseDto<bool>> Create([FromBody] CreateRoleCommand Command)
        {
            var result = await _mediator.Send(Command);
            return result;
        }

        [HasPermission(Permissions.DeleteRole)]
        [HttpDelete("{id}")]
        public async Task<ResponseDto<bool>> Delete(Guid id)
        {
            var Command = new DeleteRoleCommand(id);
            var result = await _mediator.Send(Command);
            return result;
        }

        [HasPermission(Permissions.AssignPermissionToRole)]
        [HttpPost("Permissions")]
        public async Task<ResponseDto<bool>> AssignPermissions([FromBody] AssignPermissionsCommand Command)
        {
            var result = await _mediator.Send(Command);
            return result;
        }

        [HasPermission(Permissions.UnassignPermissionFromRole)]
        [HttpDelete("Permission")]
        public async Task<ResponseDto<bool>> UnassignPermission([FromBody] UnassignPermissionCommand Command)
        {
            var result = await _mediator.Send(Command);
            return result;
        }

        [HasPermission(Permissions.GetAllRoles)]
        [HttpGet("AllRoles")]
        public async Task<ResponseDto<IEnumerable<RoleDto>>> GetAllRoles([FromQuery] GetAllRolesQuery Query)
        {
            var result = await _mediator.Send(Query);
            return result;
        }

    }
}
