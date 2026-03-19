using Application.DTOs.ResponseDTOs;
using Domain.Enums;
using MediatR;

namespace Application.Features.Roles.Commands.AssignPermissions
{
    public record AssignPermissionsCommand(Guid roleId, IEnumerable<Permissions> Permissions) : IRequest<ResponseDto<bool>>;
    
}
