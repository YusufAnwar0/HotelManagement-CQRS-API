using Application.DTOs.ResponseDTOs;
using Domain.Enums;
using MediatR;

namespace Application.Features.Roles.Commands.UnassignPermission
{
    public record UnassignPermissionCommand(Guid roleId, Permissions Permission) : IRequest<ResponseDto<bool>>;
    
}
