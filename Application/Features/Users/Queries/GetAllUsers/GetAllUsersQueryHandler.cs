using Application.DTOs.ResponseDTOs;
using Application.Features.Users.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.IRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Users.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, ResponseDto<IEnumerable<UserDto>>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<ResponseDto<IEnumerable<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAll().ProjectTo<UserDto>(_mapper.ConfigurationProvider).ToListAsync();
            return ResponseDto<IEnumerable<UserDto>>.Success(users);
        }
    }
}
