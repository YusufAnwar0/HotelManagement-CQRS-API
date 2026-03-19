using Domain.Enums;
using Domain.IRepositories;
using Domain.Models;
using FluentValidation;

namespace Application.Features.RoomTypes.Commands.AddFacilitiesToRoomType
{
    public class AddFacilitiesToRoomTypeCommandValidator : AbstractValidator<AddFacilitiesToRoomTypeCommand>
    {

        public AddFacilitiesToRoomTypeCommandValidator()
        {

            RuleFor(x => x.roomTypeId).NotEmpty().WithMessage("Room Type Id Can't be empty").WithState(_ => ErrorCode.RoomTypeRequired);

            RuleFor(x => x.facilityIds).NotEmpty().WithMessage("Facility Id Can't be empty").WithState(_ => ErrorCode.FacilityRequired);

        }
    }
}
