using Application.DTOs.ResponseDTOs;
using MediatR;

namespace Application.Features.Users.Commands.UnassignRole
{
    public record UnassignRoleCommand (Guid userId, Guid roleId) : IRequest<ResponseDto<bool>>;
}
