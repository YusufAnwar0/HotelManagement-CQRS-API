using Domain.Enums;
using FluentValidation;

namespace Application.Features.Facilities.Commands.UpdateFacility
{
    public class UpdateFacilityCommandValidator : AbstractValidator<UpdateFacilityCommand>
    {
        public UpdateFacilityCommandValidator()
        {
            RuleFor(x => x.id).NotEmpty().WithMessage("Facility Id Can't be empty").WithState(_ => ErrorCode.FacilityIdRequired);
        }
    }
}
