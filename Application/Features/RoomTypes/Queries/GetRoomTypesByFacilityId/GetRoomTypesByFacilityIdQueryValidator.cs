using Domain.Enums;
using FluentValidation;

namespace Application.Features.RoomTypes.Queries.GetRoomTypesByFacilityId
{
    public class GetRoomTypesByFacilityIdQueryValidator : AbstractValidator<GetRoomTypesByFacilityIdQuery>
    {

        public GetRoomTypesByFacilityIdQueryValidator()
        {
            RuleFor(x => x.facilityId).NotEmpty().WithMessage("facility Id Can't be Empty").WithState(_ => ErrorCode.FacilityIdRequired);
        }
    }
}
