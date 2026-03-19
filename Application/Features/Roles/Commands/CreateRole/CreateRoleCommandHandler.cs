using Application.DTOs.ResponseDTOs;
using Domain.Enums;
using Domain.IRepositories;
using Domain.Models;
using MediatR;

namespace Application.Features.Roles.Commands.CreateRole
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, ResponseDto<bool>>
    {
        private readonly IRoleRepository _roleRepository;
        public CreateRoleCommandHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public async Task<ResponseDto<bool>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var normalizedRole = request.roleName.Trim().ToUpperInvariant();
            var roleExist = await _roleRepository.IsExistAsync(x => x.Name == normalizedRole);

            if (roleExist)
                return ResponseDto<bool>.Fail(ErrorCode.RoleAlreadyExists, "Role Already Exist");

            await _roleRepository.AddAsync(new Role { Name = normalizedRole });
            await _roleRepository.SaveChanges();

            return ResponseDto<bool>.Success(true);
                                            
        }
    }
}
