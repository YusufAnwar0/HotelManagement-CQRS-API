using Application.DTOs.ResponseDTOs;
using Domain.Enums;
using Domain.IRepositories;
using Domain.Models;
using MediatR;

namespace Application.Features.Facilities.Commands.CreateFacility
{
    public class CreateFacilityCommandHandler : IRequestHandler<CreateFacilityCommand, ResponseDto<bool>>
    {
        private readonly IGeneralRepository<Facility> _facilityRepository;

        public CreateFacilityCommandHandler(IGeneralRepository<Facility> facilityRepository)
        {
            _facilityRepository = facilityRepository;
        }
        public async Task<ResponseDto<bool>> Handle(CreateFacilityCommand request, CancellationToken cancellationToken)
        {
            var facilityExist = await _facilityRepository.IsExistAsync(x => x.Code == request.Code);
            if (facilityExist)
                return ResponseDto<bool>.Fail(ErrorCode.FacilityAlreadyExists, "Facility Already Exists");

            var facility = new Facility()
            {
                Name = request.Name,
                Description = request.Description,
                Code = request.Code,
            };
            await _facilityRepository.AddAsync(facility);
            await _facilityRepository.SaveChanges();

            return ResponseDto<bool>.Success(true);
        }
    }
}
