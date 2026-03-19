using Application.DTOs.ResponseDTOs;
using Domain.Enums;
using Domain.IRepositories;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.RoomTypes.Commands.AddFacilitiesToRoomType
{
    public class AddFacilitiesToRoomTypeCommandHandler : IRequestHandler<AddFacilitiesToRoomTypeCommand, ResponseDto<bool>>
    {
        private readonly IGeneralRepository<RoomTypeFacility> _roomTypeFacilityRepository;
        private readonly IGeneralRepository<Facility> _facilityRepository;
        private readonly IGeneralRepository<RoomType> _roomTypeRepository;

        public AddFacilitiesToRoomTypeCommandHandler(IGeneralRepository<RoomTypeFacility> RoomTypeFacilityRepository, IGeneralRepository<Facility> facilityRepository, IGeneralRepository<RoomType> roomTypeRepository)
        {
            _roomTypeRepository = roomTypeRepository;
            _roomTypeFacilityRepository = RoomTypeFacilityRepository;
            _facilityRepository = facilityRepository;
        }
        public async Task<ResponseDto<bool>> Handle(AddFacilitiesToRoomTypeCommand request, CancellationToken cancellationToken)
        {
            var distinctFacilityIds = request.facilityIds.Distinct().ToList();

            var facilitiesCount = await _facilityRepository.GetAll()
                .CountAsync(f => distinctFacilityIds.Contains(f.Id));

            if(facilitiesCount != distinctFacilityIds.Count)
                return ResponseDto<bool>.Fail(ErrorCode.FacilityNotFound, "One or more facilities do not exist");


            var roomTypeFacilityIds = await _roomTypeRepository.GetAll().Where(rt => rt.Id == request.roomTypeId)
                                                                       .SelectMany(rt => rt.RoomTypeFacilities)
                                                                       .Select(rtf => rtf.FacilityId).ToListAsync();
            
            if(roomTypeFacilityIds == null)
                return ResponseDto<bool>.Fail(ErrorCode.RoomTypeNotFound, "RoomType Not Found");

            var facilitiyIds = distinctFacilityIds.Except(roomTypeFacilityIds);

            if (!facilitiyIds.Any())
                return ResponseDto<bool>.Fail(ErrorCode.FacilityAlreadyExists, "Facility Already Exist");

            var facilitiesToAdd = facilitiyIds.Select(f => new RoomTypeFacility {FacilityId = f, RoomTypeId = request.roomTypeId });

            await _roomTypeFacilityRepository.AddRangeAsync(facilitiesToAdd);
            await _roomTypeFacilityRepository.SaveChanges();

            return ResponseDto<bool>.Success(true);
        }
    }
}
