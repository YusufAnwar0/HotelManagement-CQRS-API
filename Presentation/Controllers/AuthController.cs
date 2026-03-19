using Application.DTOs.ResponseDTOs;
using Application.Features.Authentication.Commands.Login;
using Application.Features.Authentication.Commands.Register;
using Application.Features.Authentication.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<ResponseDto<bool>> Register([FromBody] RegisterCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpPost("login")]
        public async Task<ResponseDto<LoginResponseDto>> Login([FromBody] LoginCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

    }
}
