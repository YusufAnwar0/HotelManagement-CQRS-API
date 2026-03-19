using Application.DTOs.ResponseDTOs;
using AutoMapper;
using Domain.Enums;
using Domain.IRepositories;
using Domain.Models;
using MediatR;

namespace Application.Features.RoomTypes.Commands.UpdateRoomType
{
    public class UpdateRoomTypeCommandHandler : IRequestHandler<UpdateRoomTypeCommand, ResponseDto<bool>>
    {
        private readonly IGeneralRepository<RoomType> _roomTypeRepository;
        private readonly IMapper _mapper;

        public UpdateRoomTypeCommandHandler(IGeneralRepository<RoomType> roomTypeRepository, IMapper mapper)
        {
            _roomTypeRepository = roomTypeRepository;
            _mapper = mapper;
        }
        public async Task<ResponseDto<bool>> Handle(UpdateRoomTypeCommand request, CancellationToken cancellationToken)
        {
            var roomType = await _roomTypeRepository.GetWithTrackingById(request.Id);

            if (roomType == null)
                return ResponseDto<bool>.Fail(ErrorCode.RoomTypeNotFound, "Room Type Not Found");

            _mapper.Map(request, roomType);
            await _roomTypeRepository.SaveChanges();

            return ResponseDto<bool>.Success(true);
        }
    }
}
