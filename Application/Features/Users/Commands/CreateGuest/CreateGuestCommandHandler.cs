using Application.DTOs.ResponseDTOs;
using Application.Interface;
using Domain.Constants;
using Domain.Enums;
using Domain.IRepositories;
using Domain.Models;
using MediatR;

namespace Application.Features.Users.Commands.CreateGuest
{
    public class CreateGuestCommandHandler : IRequestHandler<CreateGuestCommand, ResponseDto<Guid>>
    {
        private readonly IGeneralRepository<User> _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public CreateGuestCommandHandler(IGeneralRepository<User> userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }
        public async Task<ResponseDto<Guid>> Handle(CreateGuestCommand request, CancellationToken cancellationToken)
        {
            var userExist = await _userRepository.IsExistAsync(u => u.PhoneNumber == request.phoneNumber);
            if (userExist)
                return ResponseDto<Guid>.Fail(ErrorCode.UserAlreadyExists, $"User With Phone Number: '{request.phoneNumber}' Already Exists.");

            var user = new User
            {
                Id = new Guid(),
                PhoneNumber = request.phoneNumber,
                UserName = request.name,
                Email = null,
                PasswordHash = _passwordHasher.Hash("12345"),
                Country = request.country,
                UserRoles = new List<UserRole> { new UserRole { RoleId = RoleConstants.CustomerRoleId } }
            };

            await _userRepository.AddAsync(user);
            return ResponseDto<Guid>.Success(user.Id);
        }
    }
}
