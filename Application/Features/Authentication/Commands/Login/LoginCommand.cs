using Application.DTOs.ResponseDTOs;
using Application.Features.Authentication.DTOs;
using MediatR;

namespace Application.Features.Authentication.Commands.Login
{
    public record LoginCommand(string Email, string Password) : IRequest<ResponseDto<LoginResponseDto>>;
    
}
