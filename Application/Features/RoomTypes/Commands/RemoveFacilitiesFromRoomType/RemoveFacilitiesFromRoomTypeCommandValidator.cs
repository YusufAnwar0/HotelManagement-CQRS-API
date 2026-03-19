using Domain.Enums;
using FluentValidation;

namespace Application.Features.RoomTypes.Commands.RemoveFacilitiesFromRoomType
{
    public class RemoveFacilitiesFromRoomTypeCommandValidator : AbstractValidator<RemoveFacilitiesFromRoomTypeCommand>
    {

        public RemoveFacilitiesFromRoomTypeCommandValidator()
        {
            RuleFor(x => x.roomTypeId).NotEmpty().WithMessage("Room Type Id Can't be empty").WithState(_ => ErrorCode.RoomTypeRequired);
                            
            RuleFor(x => x.facilityIds).NotEmpty().WithMessage("Facility Ids Can't be empty").WithState(_ => ErrorCode.FacilityRequired);
        }
    }
}
