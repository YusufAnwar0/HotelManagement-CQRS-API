using Application.DTOs.ResponseDTOs;
using Application.Interface;
using Domain.Enums;
using Domain.IRepositories;
using Domain.Models;
using MediatR;

namespace Application.Features.Users.Commands.CreateEmployee
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, ResponseDto<bool>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IPasswordHasher _passwordHasher;

        public CreateEmployeeCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _roleRepository = roleRepository;
        }
        public async Task<ResponseDto<bool>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var normalizedEmail = request.Email.Trim().ToLowerInvariant();
            var emailExist = await _userRepository.IsExistAsync(u => u.Email == normalizedEmail);

            if (emailExist)
                return ResponseDto<bool>.Fail(ErrorCode.EmailAlreadyExists, "Email Already Exist");

            var roleExist = await _roleRepository.IsExistAsync(r => r.Id == request.roleId);
            if (!roleExist)
                return ResponseDto<bool>.Fail(ErrorCode.RoleNotFound, "Role Not Found");

            var user = new User
            {
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                Country = request.Country,
                PasswordHash = _passwordHasher.Hash(request.Password),
                UserRoles = new List<UserRole> { new UserRole { RoleId = request.roleId } }
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChanges();

            return ResponseDto<bool>.Success(true);
        }
    }
}
