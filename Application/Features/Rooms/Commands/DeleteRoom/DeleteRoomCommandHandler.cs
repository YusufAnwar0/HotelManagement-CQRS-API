using Application.DTOs.ResponseDTOs;
using Domain.Enums;
using Domain.IRepositories;
using Domain.Models;
using MediatR;

namespace Application.Features.Rooms.Commands.DeleteRoom
{
    public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand, ResponseDto<bool>>
    {
        private readonly IGeneralRepository<Room> _roomRepository;

        public DeleteRoomCommandHandler(IGeneralRepository<Room> roomRepository)
        {
            _roomRepository = roomRepository;
        }
        public async Task<ResponseDto<bool>> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            var roomExist = await _roomRepository.IsExistAsync(r => r.Id == request.roomId);
            if (!roomExist)
                return ResponseDto<bool>.Fail(ErrorCode.RoomNotFound, "Room Not Found");

            _roomRepository.Delete(request.roomId);
            await _roomRepository.SaveChanges();

            return ResponseDto<bool>.Success(true);
        }
    }
}
