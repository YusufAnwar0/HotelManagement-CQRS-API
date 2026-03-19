using Application.DTOs.ResponseDTOs;
using MediatR;

namespace Application.Features.Authentication.Commands.Register
{
    public record RegisterCommand(string UserName, string PhoneNumber,string Country,string Email,string Password, string ConfirmPassword) : IRequest<ResponseDto<bool>>;
}
