using Application.DTOs.ResponseDTOs;
using Application.Features.Authentication.DTOs;
using Application.Interface;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Enums;
using Domain.IRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Authentication.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, ResponseDto<LoginResponseDto>>
    {
        private readonly IJwtProvider _jwtProvider;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public LoginCommandHandler(IJwtProvider jwtProvider, IPasswordHasher passwordHasher, IMapper mapper, IUserRepository userRepository)
        {
            _jwtProvider = jwtProvider;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<ResponseDto<LoginResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            string normalizedEmail = request.Email.Trim().ToLowerInvariant();

            var userCredentials = await _userRepository.GetAll().Where(u => u.Email == normalizedEmail)
                .ProjectTo<UserCredentialsDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);

            if(userCredentials == null || !_passwordHasher.Verify(request.Password, userCredentials.PasswordHash))
                return ResponseDto<LoginResponseDto>.Fail(ErrorCode.InvalidCredentials, "Invalid email or password");

            var token = _jwtProvider.GenerateToken(userCredentials.Id, userCredentials.RoleIds);

            var LoginResponse = new LoginResponseDto
            {
                Name = userCredentials.UserName,
                Token = token
            };

            return ResponseDto<LoginResponseDto>.Success(LoginResponse);

        }
    }
}
