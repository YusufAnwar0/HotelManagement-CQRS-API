using Application.DTOs.ResponseDTOs;
using MediatR;

namespace Application.Features.Users.Commands.AssignRole
{
    public record AssignRoleCommand(Guid userId, Guid roleId) : IRequest<ResponseDto<bool>>;
    
}
