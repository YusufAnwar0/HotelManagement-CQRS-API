using Application.DTOs.ResponseDTOs;
using Domain.Enums;
using Domain.IRepositories;
using Domain.Models;
using MediatR;

namespace Application.Features.Roles.Commands.UnassignPermission
{
    public class UnassignPermissionCommandHandler : IRequestHandler<UnassignPermissionCommand, ResponseDto<bool>>
    {
        private readonly IGeneralRepository<RolePermission> _rolePermissionRepository;
        public UnassignPermissionCommandHandler(IGeneralRepository<RolePermission> rolePermissionRepository)
        {
            _rolePermissionRepository = rolePermissionRepository;
        }
        public async Task<ResponseDto<bool>> Handle(UnassignPermissionCommand request, CancellationToken cancellationToken)
        {
            var rolePermissionId = await _rolePermissionRepository.GetIdAsync(x => x.RoleId == request.roleId && x.Permission == request.Permission);
            if (rolePermissionId == Guid.Empty)
                return ResponseDto<bool>.Fail(ErrorCode.RolePermissionNotFound, "Role not assign to this permission");

            _rolePermissionRepository.Delete(rolePermissionId);
            await _rolePermissionRepository.SaveChanges();

            return ResponseDto<bool>.Success(true);
        }
    }
}
