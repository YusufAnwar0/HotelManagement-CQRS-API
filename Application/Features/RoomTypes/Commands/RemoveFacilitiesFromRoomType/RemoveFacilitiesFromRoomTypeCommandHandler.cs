using Application.DTOs.ResponseDTOs;
using Domain.Enums;
using Domain.IRepositories;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.RoomTypes.Commands.RemoveFacilitiesFromRoomType
{
    public class RemoveFacilitiesFromRoomTypeCommandHandler : IRequestHandler<RemoveFacilitiesFromRoomTypeCommand, ResponseDto<bool>>
    {
        private readonly IGeneralRepository<RoomTypeFacility> _roomTypeFacilityRepository;
        private readonly IGeneralRepository<RoomType> _roomTypeRepository;

        public RemoveFacilitiesFromRoomTypeCommandHandler(IGeneralRepository<RoomTypeFacility> roomTypeFacilityRepository, IGeneralRepository<RoomType> roomTypeRepository)
        {
            _roomTypeFacilityRepository = roomTypeFacilityRepository;
            _roomTypeRepository = roomTypeRepository;
        }
        public async Task<ResponseDto<bool>> Handle(RemoveFacilitiesFromRoomTypeCommand request, CancellationToken cancellationToken)
        {
            var distinctFacilityIds = request.facilityIds.Distinct().ToList();

            var roomTypeData = await _roomTypeRepository.GetAll().Where(rt => rt.Id == request.roomTypeId)
                                                                .Select(rt => new
                                                                {
                                                                    FacilitiesToDelete = rt.RoomTypeFacilities
                                                                                            .Where(rtf => distinctFacilityIds.Contains(rtf.FacilityId))
                                                                                            .Select(rtf => rtf.Id)
                                                                                            .ToList()
                                                                }).FirstOrDefaultAsync();
              
            if(roomTypeData == null)
                return ResponseDto<bool>.Fail(ErrorCode.RoomTypeNotFound, "Room Type Not Found");

            if (distinctFacilityIds.Count != roomTypeData.FacilitiesToDelete.Count)
                return ResponseDto<bool>.Fail(ErrorCode.FacilityNotFound, "One or more facilities do not exist");

            _roomTypeFacilityRepository.DeleteRange(roomTypeData.FacilitiesToDelete);
            await _roomTypeFacilityRepository.SaveChanges();

            return ResponseDto<bool>.Success(true);
        }
    }
}
