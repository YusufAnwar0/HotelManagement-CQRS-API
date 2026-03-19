using Application.DTOs.ResponseDTOs;
using Application.Features.Roles.DTOs;
using MediatR;

namespace Application.Features.Roles.Queries.GetAllRoles
{
    public record GetAllRolesQuery() : IRequest<ResponseDto<IEnumerable<RoleDto>>>;
    
}
