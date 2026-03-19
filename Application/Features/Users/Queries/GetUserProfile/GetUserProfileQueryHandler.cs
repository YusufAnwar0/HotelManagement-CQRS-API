using Application.DTOs.ResponseDTOs;
using Application.Features.Users.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Enums;
using Domain.IRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Users.Queries.GetUserProfile
{
    public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, ResponseDto<UserProfileDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public GetUserProfileQueryHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public async Task<ResponseDto<UserProfileDto>> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAll()
                .Where(u => u.Id == request.userId)
                .ProjectTo<UserProfileDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (user == null)
                return ResponseDto<UserProfileDto>.Fail(ErrorCode.UserNotFound, "user not found");

            return ResponseDto<UserProfileDto>.Success(user);
        }
    }
}
