using Application.DTOs.ResponseDTOs;
using Application.Features.RoomTypes.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.IRepositories;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.RoomTypes.Queries.GetRoomTypesByFacilityId;

public class GetRoomTypesByFacilityIdQueryHandler : IRequestHandler<GetRoomTypesByFacilityIdQuery, ResponseDto<IEnumerable<GetRoomTypeDTO>>>
{
    private readonly IGeneralRepository<RoomType> _roomTypeRepository;
    private readonly IMapper _mapper;
    private readonly IGeneralRepository<RoomTypeFacility> _roomTypeFacilityRepository;

    public GetRoomTypesByFacilityIdQueryHandler(IGeneralRepository<RoomType> roomTypeRepository, IMapper mapper, IGeneralRepository<RoomTypeFacility> roomTypeFacilityRepository)
    {
        _roomTypeRepository = roomTypeRepository;
        _mapper = mapper;
        _roomTypeFacilityRepository = roomTypeFacilityRepository;
    }
    public async Task<ResponseDto<IEnumerable<GetRoomTypeDTO>>> Handle(GetRoomTypesByFacilityIdQuery request, CancellationToken cancellationToken)
    {

        var roomTypesDto = await _roomTypeRepository.GetAll()
            .Where(rt => rt.RoomTypeFacilities.Any(rtf => rtf.FacilityId == request.facilityId))
            .ProjectTo<GetRoomTypeDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return ResponseDto<IEnumerable<GetRoomTypeDTO>>.Success(roomTypesDto);
    }
}
