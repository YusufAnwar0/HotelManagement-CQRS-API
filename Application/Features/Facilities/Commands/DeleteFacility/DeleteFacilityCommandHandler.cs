using Application.DTOs.ResponseDTOs;
using Domain.Enums;
using Domain.IRepositories;
using Domain.Models;
using MediatR;
using static System.Net.Mime.MediaTypeNames;

namespace Application.Features.Facilities.Commands.DeleteFacility
{
    public class DeleteFacilityCommandHandler : IRequestHandler<DeleteFacilityCommand, ResponseDto<bool>>
    {
        private readonly IGeneralRepository<Facility> _facilityRepository;
        public DeleteFacilityCommandHandler(IGeneralRepository<Facility> facilityRepository)
        {
            _facilityRepository = facilityRepository;
        }
        public async Task<ResponseDto<bool>> Handle(DeleteFacilityCommand request, CancellationToken cancellationToken)
        {
            var facilityExist = await _facilityRepository.IsExistAsync(x => x.Id == request.facilityId);
            if (!facilityExist)
                return ResponseDto<bool>.Fail(ErrorCode.FacilityNotFound, "Facility Not Found");

            _facilityRepository.Delete(request.facilityId);
            await _facilityRepository.SaveChanges();

            return ResponseDto<bool>.Success(true);
        }
    }
}
