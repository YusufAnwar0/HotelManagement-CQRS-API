using Application.DTOs.ResponseDTOs;
using AutoMapper;
using Domain.Enums;
using Domain.IRepositories;
using Domain.Models;
using MediatR;

namespace Application.Features.Rooms.Commands.CreateRoom
{
    public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, ResponseDto<bool>>
    {
        private readonly IGeneralRepository<Room> _roomRepository;
        private readonly IGeneralRepository<RoomType> _roomTypeRepository;
        private readonly IMapper _mapper;

        public CreateRoomCommandHandler(IGeneralRepository<Room> roomRepository, IGeneralRepository<RoomType> roomTypeRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _roomTypeRepository = roomTypeRepository;
            _mapper = mapper;
        }
        public async Task<ResponseDto<bool>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            var roomNumberExist = await _roomRepository.IsExistAsync(r => r.RoomNumber == request.RoomNumber);
            if (roomNumberExist)
                return ResponseDto<bool>.Fail(ErrorCode.RoomNumberAlreadyExists, "Room Number Already Exists");

            var roomTypeExists = await _roomTypeRepository.IsExistAsync(rt => rt.Id == request.RoomTypeId);
            if (!roomTypeExists)
                return ResponseDto<bool>.Fail(ErrorCode.RoomTypeNotFound, "Room Type Not Found");

            var room = _mapper.Map<Room>(request);

            await _roomRepository.AddAsync(room);
            await _roomRepository.SaveChanges();

            return ResponseDto<bool>.Success(true);
        }
    }
}
