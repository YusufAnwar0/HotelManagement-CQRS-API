using Domain.Enums;
using FluentValidation;

namespace Application.Features.Facilities.Commands.DeleteFacility
{
    public class DeleteFacilityCommandValidator : AbstractValidator<DeleteFacilityCommand>
    {
        public DeleteFacilityCommandValidator()
        {
            RuleFor(x => x.facilityId).NotEmpty().WithMessage("Facility Id Can't be empty").WithState(_ => ErrorCode.FacilityIdRequired);
        }
    }
}
