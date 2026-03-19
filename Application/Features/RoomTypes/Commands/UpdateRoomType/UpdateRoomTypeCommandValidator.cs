using Domain.Enums;
using FluentValidation;

namespace Application.Features.RoomTypes.Commands.UpdateRoomType
{
    public class UpdateRoomTypeCommandValidator : AbstractValidator<UpdateRoomTypeCommand>
    {

        public UpdateRoomTypeCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("RoomType Id Can't be empty").WithState(_ => ErrorCode.RoomTypeIdRequired);
        }
    }
}
