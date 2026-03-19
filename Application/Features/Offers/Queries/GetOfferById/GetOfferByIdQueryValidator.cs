using Domain.Enums;
using FluentValidation;

namespace Application.Features.Offers.Queries.GetOfferById
{
    public class GetOfferByIdQueryValidator : AbstractValidator<GetOfferByIdQuery>
    {
        public GetOfferByIdQueryValidator()
        {
            RuleFor(x => x.offerId).NotEmpty().WithMessage("Offer Id Can't be empty").WithState(_ => ErrorCode.OfferIdRequired);
        }

    }
}
