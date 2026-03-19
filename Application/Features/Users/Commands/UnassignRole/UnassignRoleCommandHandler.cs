using Application.DTOs.ResponseDTOs;
using Domain.Enums;
using Domain.IRepositories;
using Domain.Models;
using MediatR;

namespace Application.Features.Users.Commands.UnassignRole
{
    public class UnassignRoleCommandHandler : IRequestHandler<UnassignRoleCommand, ResponseDto<bool>>
    {
        private readonly IGeneralRepository<UserRole> _userRoleRepository;

        public UnassignRoleCommandHandler(IGeneralRepository<UserRole> userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }
        public async Task<ResponseDto<bool>> Handle(UnassignRoleCommand request, CancellationToken cancellationToken)
        {
            var id = await _userRoleRepository.GetIdAsync(x => x.UserId == request.userId && x.RoleId == request.roleId);

            if (id == Guid.Empty)
                return ResponseDto<bool>.Fail(ErrorCode.UserRoleNotFound, "This role is not assigned to this user");

            _userRoleRepository.Delete(id);
            await _userRoleRepository.SaveChanges();

            return ResponseDto<bool>.Success(true);

        }
    }
}
