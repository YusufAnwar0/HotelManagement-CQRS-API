using Application.DTOs.ResponseDTOs;
using MediatR;

namespace Application.Features.Users.Commands.CreateEmployee
{
    public record CreateEmployeeCommand(string UserName, string PhoneNumber, string Country, string Email, string Password, string ConfirmPassword, Guid roleId) : IRequest<ResponseDto<bool>>;
    
}
