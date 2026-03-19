using Domain.Enums;
using FluentValidation;

namespace Application.Features.RoomTypes.Queries.GetRoomType
{
    public class GetRoomTypeQueryValidator : AbstractValidator<GetRoomTypeQuery>
    {
        public GetRoomTypeQueryValidator()
        {
            RuleFor(x => x.roomTypeId).NotEmpty().WithMessage("Room Type Id Can't be Empty").WithState(_ => ErrorCode.RoomTypeIdRequired);
        }
    }
}
