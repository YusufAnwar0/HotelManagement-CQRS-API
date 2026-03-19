using Application.DTOs.ResponseDTOs;
using Application.Features.RoomTypes.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Enums;
using Domain.IRepositories;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.RoomTypes.Queries.GetRoomType
{
    public class GetRoomTypeQueryHandler : IRequestHandler<GetRoomTypeQuery, ResponseDto<GetRoomTypeDTO>>
    {
        private readonly IGeneralRepository<RoomType> _roomTypeRepository;
        private readonly IMapper _mapper;

        public GetRoomTypeQueryHandler(IGeneralRepository<RoomType> roomTypeRepository, IMapper mapper)
        {
            _roomTypeRepository = roomTypeRepository;
            _mapper = mapper;
        }
        public async Task<ResponseDto<GetRoomTypeDTO>> Handle(GetRoomTypeQuery request, CancellationToken cancellationToken)
        {
            var roomTypeDto = await _roomTypeRepository.GetAll()
                .Where(rt => rt.Id == request.roomTypeId)
                .ProjectTo<GetRoomTypeDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (roomTypeDto == null)
                return ResponseDto<GetRoomTypeDTO>.Fail(ErrorCode.RoomTypeNotFound, "Room Type Not Found");

            return ResponseDto<GetRoomTypeDTO>.Success(roomTypeDto);
        }
    }
}
