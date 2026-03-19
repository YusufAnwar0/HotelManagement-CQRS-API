using Application.DTOs.ResponseDTOs;
using Application.Features.Users.Commands.AssignRole;
using Application.Features.Users.Commands.CreateEmployee;
using Application.Features.Users.Commands.DeleteUser;
using Application.Features.Users.Commands.UnassignRole;
using Application.Features.Users.DTOs;
using Application.Features.Users.Queries.GetAllUsers;
using Application.Features.Users.Queries.GetUserProfile;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HasPermission(Permissions.AssignRoleToUser)]
        [HttpPost("Role")]
        public async Task<ResponseDto<bool>> AssignRole([FromBody] AssignRoleCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        [HasPermission(Permissions.UnassignRoleFromUser)]
        [HttpDelete("Role")]
        public async Task<ResponseDto<bool>> UnassignRole([FromBody] UnassignRoleCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        [HasPermission(Permissions.CreateStaffUser)]
        [HttpPost("Employee")]
        public async Task<ResponseDto<bool>> CreateEmployee([FromBody] CreateEmployeeCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        [HasPermission(Permissions.DeleteUser)]
        [HttpDelete("User")]
        public async Task<ResponseDto<bool>> DeleteUser([FromBody] DeleteUserCommand command)
        {
            var result = await _mediator.Send(command); 
            return result;
        }

        [HasPermission(Permissions.ViewOwnProfile)]
        [HttpGet("userProfile")]
        public async Task<ResponseDto<UserProfileDto>> UserProfile([FromQuery] GetUserProfileQuery query)
        {
            var result = await _mediator.Send(query);
            return result;
        }

        [HasPermission(Permissions.GetAllUsers)]
        [HttpGet("AllUsers")]
        public async Task<ResponseDto<IEnumerable<UserDto>>> GetAllUsers([FromQuery] GetAllUsersQuery query)
        {
            var result = await _mediator.Send(query);
            return result;
        }
    }
}
