using Application.DTOs.ResponseDTOs;
using AutoMapper;
using Domain.Enums;
using Domain.IRepositories;
using Domain.Models;
using MediatR;

namespace Application.Features.RoomTypes.Commands.CreateRoomType
{
    public class CreateRoomTypeCommandHandler : IRequestHandler<CreateRoomTypeCommand, ResponseDto<bool>>
    {
        private readonly IGeneralRepository<RoomType> _roomTypeRepository;
        private readonly IMapper _mapper;

        public CreateRoomTypeCommandHandler(IGeneralRepository<RoomType> roomTypeRepository, IMapper mapper)
        {
            _roomTypeRepository = roomTypeRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDto<bool>> Handle(CreateRoomTypeCommand request, CancellationToken cancellationToken)
        {
            var roomTypeExist = await _roomTypeRepository.IsExistAsync(rt => rt.Code == request.Code);
            if(roomTypeExist)
                return ResponseDto<bool>.Fail(ErrorCode.RoomTypeAlreadyExists, "Room Type Already Exists");

            var roomType = _mapper.Map<RoomType>(request);

            await _roomTypeRepository.AddAsync(roomType);
            await _roomTypeRepository.SaveChanges();

            return ResponseDto<bool>.Success(true);

        }
    }
}
