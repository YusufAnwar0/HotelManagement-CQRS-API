using Application.DTOs.ResponseDTOs;
using Application.Interface;
using Domain.Constants;
using Domain.Enums;
using Domain.IRepositories;
using Domain.Models;
using MediatR;
using System.Collections;

namespace Application.Features.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ResponseDto<bool>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public RegisterCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }
        public async Task<ResponseDto<bool>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var emailExist = await _userRepository.IsExistAsync(u => u.Email == request.Email);
            if (emailExist)
                return ResponseDto<bool>.Fail(ErrorCode.EmailAlreadyExists, "Email Already Exist");

            var user = new User
            {
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                Country = request.Country,
                PasswordHash = _passwordHasher.Hash(request.Password),
                UserRoles = new List<UserRole> { new UserRole { RoleId = RoleConstants.CustomerRoleId }}
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChanges();

            return ResponseDto<bool>.Success(true);
        }
    }
}
