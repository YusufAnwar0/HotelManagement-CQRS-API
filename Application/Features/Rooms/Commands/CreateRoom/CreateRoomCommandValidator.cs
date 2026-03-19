using Domain.Enums;
using FluentValidation;

namespace Application.Features.Rooms.Commands.CreateRoom
{
    public class CreateRoomCommandValidator : AbstractValidator<CreateRoomCommand>
    {
        public CreateRoomCommandValidator()
        {
            RuleFor(r => r.RoomNumber).NotEmpty().WithMessage("Room Not Found").WithState(_ => ErrorCode.RoomNumberRequired);
            RuleFor(r => r.Status).NotEmpty().WithMessage("Room Status Can't be Empty").WithState(_ => ErrorCode.RoomStatusRequired).IsInEnum();
            RuleFor(r => r.RoomTypeId).NotEmpty().WithMessage("RoomType Id Can't be Empty").WithState(_ => ErrorCode.RoomTypeRequired);
        }
    }
}
