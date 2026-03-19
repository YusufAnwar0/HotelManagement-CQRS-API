using Domain.Enums;
using FluentValidation;

namespace Application.Features.Rooms.Commands.DeleteRoom
{
    public class DeleteRoomCommandValidator : AbstractValidator<DeleteRoomCommand>
    {
        public DeleteRoomCommandValidator()
        {
            RuleFor(x => x.roomId).NotEmpty().WithMessage("Room Id Can't be Empty").WithState(_ => ErrorCode.RoomIdRequired);
        }
    }
}
