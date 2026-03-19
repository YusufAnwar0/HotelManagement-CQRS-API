using Application.DTOs.ResponseDTOs;
using Domain.Enums;
using Domain.IRepositories;
using MediatR;

namespace Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ResponseDto<bool>>
    {
        private readonly IUserRepository _userRepository;
        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ResponseDto<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var exist = await _userRepository.IsExistAsync(x => x.Id == request.userId);
            if (!exist)
                return ResponseDto<bool>.Fail(ErrorCode.UserNotFound, "User not found");

            _userRepository.Delete(request.userId);
            await _userRepository.SaveChanges();

            return ResponseDto<bool>.Success(true);
        }
    }
}
