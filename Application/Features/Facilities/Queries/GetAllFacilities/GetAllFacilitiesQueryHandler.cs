using Application.DTOs.ResponseDTOs;
using Application.Features.Facilities.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.IRepositories;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Facilities.Queries.GetAllFacilities
{
    public class GetAllFacilitiesQueryHandler : IRequestHandler<GetAllFacilitiesQuery, ResponseDto<IEnumerable<GetFacilityDto>>>
    {
        private readonly IGeneralRepository<Facility> _facilityRepository;
        private readonly IMapper _mapper;

        public GetAllFacilitiesQueryHandler(IGeneralRepository<Facility> facilityRepository, IMapper mapper)
        {
            _facilityRepository = facilityRepository;
            _mapper = mapper;
        }
        public async Task<ResponseDto<IEnumerable<GetFacilityDto>>> Handle(GetAllFacilitiesQuery request, CancellationToken cancellationToken)
        {
            var facilities = await _facilityRepository.GetAll()
                .ProjectTo<GetFacilityDto>(_mapper.ConfigurationProvider)
                .ToListAsync(); 

            return ResponseDto<IEnumerable<GetFacilityDto>>.Success(facilities);

        }
    }
}
