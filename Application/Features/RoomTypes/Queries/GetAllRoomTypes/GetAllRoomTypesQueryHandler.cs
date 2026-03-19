using Application.DTOs.ResponseDTOs;
using Application.Features.RoomTypes.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.IRepositories;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.RoomTypes.Queries.GetAllRoomTypes
{
    public class GetAllRoomTypesQueryHandler : IRequestHandler<GetAllRoomTypesQuery, ResponseDto<IEnumerable<GetRoomTypeDTO>>>
    {
        private readonly IGeneralRepository<RoomType> _roomTypeRepository;
        private readonly IMapper _mapper;

        public GetAllRoomTypesQueryHandler(IGeneralRepository<RoomType> roomTypeRepository, IMapper mapper)
        {
            _roomTypeRepository = roomTypeRepository;
            _mapper = mapper;
        }
        public async Task<ResponseDto<IEnumerable<GetRoomTypeDTO>>> Handle(GetAllRoomTypesQuery request, CancellationToken cancellationToken)
        {
            var roomTypeDtos = await _roomTypeRepository.GetAll()
                .ProjectTo<GetRoomTypeDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return ResponseDto<IEnumerable<GetRoomTypeDTO>>.Success(roomTypeDtos);
        }
    }
}

