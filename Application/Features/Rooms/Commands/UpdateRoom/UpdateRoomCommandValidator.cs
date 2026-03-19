using Domain.Enums;
using FluentValidation;

namespace Application.Features.Rooms.Commands.UpdateRoom
{
    public class UpdateRoomCommandValidator : AbstractValidator<UpdateRoomCommand>
    {
        public UpdateRoomCommandValidator()
        {
            RuleFor(x => x.id).NotEmpty().WithMessage("Room Id is required.").WithState(_ => ErrorCode.RoomIdRequired);
        }
    }
}
