using Domain.Enums;
using FluentValidation;

namespace Application.Features.Offers.Commands.DeleteOffer
{
    public class DeleteOfferCommandValidator : AbstractValidator<DeleteOfferCommand>
    {
        public DeleteOfferCommandValidator()
        {
            RuleFor(x => x.id).NotEmpty().WithMessage("Offer Id is required.").WithState(_ => ErrorCode.OfferIdRequired);
        }
    }
}