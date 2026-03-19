using Application.DTOs.ResponseDTOs;
using AutoMapper;
using Domain.Enums;
using Domain.IRepositories;
using Domain.Models;
using MediatR;

namespace Application.Features.Rooms.Commands.UpdateRoom
{
    public class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand, ResponseDto<bool>>
    {
        private readonly IGeneralRepository<Room> _roomRepository;
        private readonly IGeneralRepository<RoomType> _roomTypeRepository;
        private readonly IMapper _mapper;

        public UpdateRoomCommandHandler(IGeneralRepository<Room> roomRepository, IGeneralRepository<RoomType> roomTypeRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _roomTypeRepository = roomTypeRepository;
            _mapper = mapper;
        }
        public async Task<ResponseDto<bool>> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            var room = await _roomRepository.GetWithTrackingById(request.id);
            if (room == null)
                return ResponseDto<bool>.Fail(ErrorCode.RoomNotFound, "Room Not Found");

            if(request.RoomTypeId != null)
            {
                var roomTypeExist = await _roomTypeRepository.IsExistAsync(rt => rt.Id == request.RoomTypeId);
                if(!roomTypeExist)
                    return ResponseDto<bool>.Fail(ErrorCode.RoomTypeNotFound, "Room Type Not Found");
            }

            _mapper.Map(request, room);

            await _roomRepository.SaveChanges();
            return ResponseDto<bool>.Success(true);
        }
    }
}
