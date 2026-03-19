using Application.DTOs.ResponseDTOs;
using Application.Features.Rooms.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Enums;
using Domain.IRepositories;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace Application.Features.Rooms.Queries.GetRoomById
{
    public class GetRoomByIdQueryHandler : IRequestHandler<GetRoomByIdQuery, ResponseDto<RoomDto>>
    {
        private readonly IGeneralRepository<Room> _roomRepository;
        private readonly IMapper _mapper;

        public GetRoomByIdQueryHandler(IGeneralRepository<Room> roomRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
        }
        public async Task<ResponseDto<RoomDto>> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
        {
            var room = await _roomRepository.GetAll()
                .Where(r => r.Id == request.roomId)
                .ProjectTo<RoomDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (room == null)
                return ResponseDto<RoomDto>.Fail(ErrorCode.RoomNotFound, "Room Not Found");

            return ResponseDto<RoomDto>.Success(room);
        }
    }
}
