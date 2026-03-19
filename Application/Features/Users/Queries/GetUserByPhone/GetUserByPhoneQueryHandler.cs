using Application.DTOs.ResponseDTOs;
using Domain.Enums;
using Domain.IRepositories;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Users.Queries.GetUserByPhone
{
    public class GetUserByPhoneQueryHandler : IRequestHandler<GetUserByPhoneQuery, ResponseDto<User>>
    {
        private readonly IGeneralRepository<User> _userRepository;

        public GetUserByPhoneQueryHandler(IGeneralRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ResponseDto<User>> Handle(GetUserByPhoneQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(u => u.PhoneNumber == request.phoneNumber);
            if (user == null)
                return ResponseDto<User>.Fail(ErrorCode.UserNotFound, "user Not Found");

            return ResponseDto<User>.Success(user);
        }
    }
}
