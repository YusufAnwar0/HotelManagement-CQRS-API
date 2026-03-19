using Application.DTOs.ResponseDTOs;
using Domain.Enums;
using Domain.IRepositories;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Roles.Commands.AssignPermissions
{
    public class AssignPermissionsCommandHandler : IRequestHandler<AssignPermissionsCommand, ResponseDto<bool>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IGeneralRepository<RolePermission> _rolePermissionRepository;
        public AssignPermissionsCommandHandler(IRoleRepository roleRepository, IGeneralRepository<RolePermission> rolePermissionRepository)
        {
            _roleRepository = roleRepository;
            _rolePermissionRepository = rolePermissionRepository;
        }
        public async Task<ResponseDto<bool>> Handle(AssignPermissionsCommand request, CancellationToken cancellationToken)
        {
            var roleExist = await _roleRepository.IsExistAsync(x => x.Id == request.roleId);
            if (!roleExist)
                return ResponseDto<bool>.Fail(ErrorCode.RoleNotFound, "Role Not Found");

            var existingPermissions = await _rolePermissionRepository.GetAll()
                .Where(x => x.RoleId == request.roleId)
                .Select(x => x.Permission).ToListAsync();

            var permissionToAdd = request.Permissions.Except(existingPermissions).ToList();

            if (!permissionToAdd.Any())
                return ResponseDto<bool>.Success(true, "All provided permissions are already assigned to this role.");

            var rolePermissions = permissionToAdd.Select(x => new RolePermission { RoleId = request.roleId, Permission = x }).ToList();

            await _rolePermissionRepository.AddRangeAsync(rolePermissions);
            await _rolePermissionRepository.SaveChanges();

            return ResponseDto<bool>.Success(true);
        }
    }
}
