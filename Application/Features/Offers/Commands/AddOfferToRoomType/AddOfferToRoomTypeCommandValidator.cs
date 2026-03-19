using Domain.Enums;
using FluentValidation;

namespace Application.Features.Offers.Commands.AddOfferToRoomType
{
    public class AddOfferToRoomTypeCommandValidator : AbstractValidator<AddOfferToRoomTypeCommand>
    {

        public AddOfferToRoomTypeCommandValidator()
        {
            RuleFor(x => x.offerId).NotEmpty().WithMessage("Offer Id Can't be empty").WithState(_ => ErrorCode.OfferIdRequired);

            RuleFor(x => x.roomTypeId).NotEmpty().WithMessage("RoomType Id Can't be empty").WithState(_ => ErrorCode.RoomTypeIdRequired);
        }
    }
}
