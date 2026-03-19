using Application.DTOs.ResponseDTOs;
using Application.Features.Rooms.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.IRepositories;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Rooms.Queries.GetAllRooms
{
    public class GetAllRoomsQueryHandler : IRequestHandler<GetAllRoomsQuery, ResponseDto<IEnumerable<RoomDto>>>
    {
        private readonly IGeneralRepository<Room> _roomRepository;
        private readonly IMapper _mapper;

        public GetAllRoomsQueryHandler(IGeneralRepository<Room> roomRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
        }
        public async Task<ResponseDto<IEnumerable<RoomDto>>> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
        {
            int pageSize = 10;
            int skip = (request.pageNumber - 1) * pageSize;

            var rooms = await _roomRepository.GetAll()
                .OrderBy(r => r.RoomNumber)
                .Skip(skip)
                .Take(pageSize)
                .ProjectTo<RoomDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return ResponseDto<IEnumerable<RoomDto>>.Success(rooms);
        }
    }
}
