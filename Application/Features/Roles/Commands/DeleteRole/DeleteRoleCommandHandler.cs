using Application.DTOs.ResponseDTOs;
using Domain.Enums;
using Domain.IRepositories;
using MediatR;

namespace Application.Features.Roles.Commands.DeleteRole
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, ResponseDto<bool>>
    {
        private readonly IRoleRepository _roleRepository;
        public DeleteRoleCommandHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;

        }
        public async Task<ResponseDto<bool>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var roleExist = await _roleRepository.IsExistAsync(x => x.Id == request.roleId);

            if (!roleExist)
                return ResponseDto<bool>.Fail(ErrorCode.RoleNotFound, "Role Not Found");

            _roleRepository.Delete(request.roleId);
            await _roleRepository.SaveChanges();

            return ResponseDto<bool>.Success(true);
        }
    }
}
