using Application.DTOs.ResponseDTOs;
using MediatR;

namespace Application.Features.Roles.Commands.CreateRole
{
    public record CreateRoleCommand(string roleName) : IRequest<ResponseDto<bool>>;
    
}
