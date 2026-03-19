using Domain.Enums;
using FluentValidation;

namespace Application.Features.Offers.Commands.CreateOffer
{
    public class CreateOfferCommandValidator : AbstractValidator<CreateOfferCommand>
    {
        public CreateOfferCommandValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("Offer Code Can't be empty").WithState(_ => ErrorCode.OfferCodeRequired);

            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.").WithState(_ => ErrorCode.OfferDescriptionRequired);

            RuleFor(x => x.StartDate).NotEmpty().WithMessage("Start date is required.").WithState(_ => ErrorCode.OfferStartDateRequired)
                .GreaterThanOrEqualTo(DateTime.UtcNow.Date).WithMessage("Start date cannot be in the past.").WithState(_ => ErrorCode.OfferInvalidStartDate);

            RuleFor(x => x.EndDate).NotEmpty().WithMessage("End date is required.").WithState(_ => ErrorCode.OfferEndDateRequired)
                .GreaterThan(x => x.StartDate).WithMessage("End date must be after the start date").WithState(_ => ErrorCode.OfferInvalidEndDate);

            RuleFor(x => x.DiscountValue).NotEmpty().WithMessage("").WithState(_ => ErrorCode.OfferDiscountValueRequired)
            .GreaterThan(0).WithMessage("Discount value must be greater than 0.").WithState(_ => ErrorCode.OfferInvalidDiscountValue);
        }
    }
}
