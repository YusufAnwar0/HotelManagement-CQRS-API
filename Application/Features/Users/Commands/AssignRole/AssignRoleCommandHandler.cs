using Application.DTOs.ResponseDTOs;
using Domain.Enums;
using Domain.IRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Users.Commands.AssignRole
{
    public class AssignRoleCommandHandler : IRequestHandler<AssignRoleCommand, ResponseDto<bool>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public AssignRoleCommandHandler(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }
        public async Task<ResponseDto<bool>> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
        {
            var roleExist = await _roleRepository.IsExistAsync(r => r.Id == request.roleId);
            if (!roleExist)
                return ResponseDto<bool>.Fail(ErrorCode.RoleNotFound, "Role Not Found");

            var userData = await _userRepository.GetAll().Where(u => u.Id == request.userId)
                                                         .Select(u => new
                                                         {
                                                             roleAssigned = u.UserRoles.Any(ur => ur.RoleId == request.roleId),
                                                         })
                                                         .FirstOrDefaultAsync();

            if (userData == null)
                return ResponseDto<bool>.Fail(ErrorCode.UserNotFound, "User Not Found");

            if (userData.roleAssigned)
                return ResponseDto<bool>.Fail(ErrorCode.DuplicateEntry, "User Already Assigned To This Role");

            await _userRepository.AssignRoleAsync(request.userId, request.roleId);
            await _userRepository.SaveChanges();
            return ResponseDto<bool>.Success(true);
        }
    }
}
