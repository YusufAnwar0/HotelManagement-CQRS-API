using Application.DTOs.ResponseDTOs;
using Domain.Enums;
using Domain.IRepositories;
using Domain.Models;
using MediatR;

namespace Application.Features.RoomTypes.Commands.DeleteRoomType
{
    public class DeleteRoomTypeCommandHandler : IRequestHandler<DeleteRoomTypeCommand, ResponseDto<bool>>
    {
        private readonly IGeneralRepository<RoomType> _roomTypeRepository;

        public DeleteRoomTypeCommandHandler(IGeneralRepository<RoomType> roomTypeRepository)
        {
            _roomTypeRepository = roomTypeRepository;
        }
        public async Task<ResponseDto<bool>> Handle(DeleteRoomTypeCommand request, CancellationToken cancellationToken)
        {
            var roomTypeExist = await _roomTypeRepository.IsExistAsync(rt => rt.Id == request.roomTypeId);
            if (!roomTypeExist)
                return ResponseDto<bool>.Fail(ErrorCode.RoomTypeNotFound, "Room Type Not Found");

            _roomTypeRepository.Delete(request.roomTypeId);
            await _roomTypeRepository.SaveChanges();

            return ResponseDto<bool>.Success(true);
        }
    }
}
