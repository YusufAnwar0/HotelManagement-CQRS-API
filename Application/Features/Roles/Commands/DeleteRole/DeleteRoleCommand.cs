using Application.DTOs.ResponseDTOs;
using MediatR;

namespace Application.Features.Roles.Commands.DeleteRole
{
    public record DeleteRoleCommand(Guid roleId) : IRequest<ResponseDto<bool>>;
    
}
