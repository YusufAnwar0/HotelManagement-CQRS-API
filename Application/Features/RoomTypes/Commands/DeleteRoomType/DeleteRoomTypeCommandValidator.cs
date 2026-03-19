using Domain.Enums;
using Domain.IRepositories;
using Domain.Models;
using FluentValidation;

namespace Application.Features.RoomTypes.Commands.DeleteRoomType
{
    public class DeleteRoomTypeCommandValidator : AbstractValidator<DeleteRoomTypeCommand>
    {
        public DeleteRoomTypeCommandValidator()
        {
            RuleFor(x => x.roomTypeId).NotEmpty().WithMessage("RoomType Id Can't be empty").WithState(_ => ErrorCode.RoomTypeIdRequired);
        }


    }
}
