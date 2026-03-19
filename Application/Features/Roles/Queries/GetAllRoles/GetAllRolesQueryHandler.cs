using Application.DTOs.ResponseDTOs;
using Application.Features.Roles.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.IRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Roles.Queries.GetAllRoles
{
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, ResponseDto<IEnumerable<RoleDto>>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        public GetAllRolesQueryHandler(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }
        public async Task<ResponseDto<IEnumerable<RoleDto>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _roleRepository.GetAll().ProjectTo<RoleDto>(_mapper.ConfigurationProvider).ToListAsync();
            return ResponseDto<IEnumerable<RoleDto>>.Success(roles);
        }
    }
}
