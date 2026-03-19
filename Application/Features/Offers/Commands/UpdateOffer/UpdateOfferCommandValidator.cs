using Domain.Enums;
using FluentValidation;

namespace Application.Features.Offers.Commands.UpdateOffer
{
    public class UpdateOfferCommandValidator : AbstractValidator<UpdateOfferCommand>
    {
        public UpdateOfferCommandValidator()
        {

            RuleFor(x => x.Id).NotEmpty().WithMessage("Offer Id is required.").WithState(_ => ErrorCode.OfferIdRequired);

            RuleFor(x => x.StartDate).GreaterThanOrEqualTo(DateTime.UtcNow.Date).When(x => x.StartDate.HasValue)
                .WithMessage("Start date cannot be in the past.").WithState(_ => ErrorCode.OfferInvalidStartDate);

            RuleFor(x => x.EndDate).GreaterThan(x => x.StartDate).When(x => x.StartDate.HasValue && x.EndDate.HasValue)
                .WithMessage("End date must be after the start date").WithState(_ => ErrorCode.OfferInvalidEndDate);

            RuleFor(x => x.DiscountValue).GreaterThan(0).When(x => x.DiscountValue.HasValue)
                .WithMessage("Discount value must be greater than 0.").WithState(_ => ErrorCode.OfferInvalidDiscountValue);
        }
    }
}
