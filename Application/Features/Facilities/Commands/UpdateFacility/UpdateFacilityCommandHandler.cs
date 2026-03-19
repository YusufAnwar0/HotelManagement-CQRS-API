using Application.DTOs.ResponseDTOs;
using AutoMapper;
using Domain.Enums;
using Domain.IRepositories;
using Domain.Models;
using MediatR;

namespace Application.Features.Facilities.Commands.UpdateFacility
{
    public class UpdateFacilityCommandHandler : IRequestHandler<UpdateFacilityCommand, ResponseDto<bool>>
    {
        private readonly IGeneralRepository<Facility> _facilityRepository;
        private readonly IMapper _mapper;

        public UpdateFacilityCommandHandler(IGeneralRepository<Facility> facilityRepository, IMapper mapper)
        {
            _facilityRepository = facilityRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDto<bool>> Handle(UpdateFacilityCommand request, CancellationToken cancellationToken)
        {
            var facility = await _facilityRepository.GetWithTrackingById(request.id);
            if (facility is null)
                return ResponseDto<bool>.Fail(ErrorCode.FacilityNotFound, "Facility Not Found");

            _mapper.Map(request, facility);

            await _facilityRepository.SaveChanges();

            return ResponseDto<bool>.Success(true);
        }
    }
}
