using Application.DTOs.ResponseDTOs;
using Application.Features.Facilities.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Enums;
using Domain.IRepositories;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Facilities.Queries.GetFacilityById
{
    public class GetFacilityByIdQueryHandler : IRequestHandler<GetFacilityByIdQuery, ResponseDto<GetFacilityDto>>
    {
        private readonly IGeneralRepository<Facility> _facilityRepository;
        private readonly IMapper _mapper;

        public GetFacilityByIdQueryHandler(IGeneralRepository<Facility> facilityRepository, IMapper mapper)
        {
            _facilityRepository = facilityRepository;
            _mapper = mapper;
        }
        public async Task<ResponseDto<GetFacilityDto>> Handle(GetFacilityByIdQuery request, CancellationToken cancellationToken)
        {
            var facility = await _facilityRepository.GetAll().Where(f => f.Id == request.id)
                .ProjectTo<GetFacilityDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (facility == null)
                return ResponseDto<GetFacilityDto>.Fail(ErrorCode.FacilityNotFound, "Facility Not Found");

            return ResponseDto<GetFacilityDto>.Success(facility);
        }
    }
}
