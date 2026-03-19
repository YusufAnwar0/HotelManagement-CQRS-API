using Domain.Enums;
using FluentValidation;

namespace Application.Features.Facilities.Commands.CreateFacility
{
    public class CreateFacilityCommandValidator : AbstractValidator<CreateFacilityCommand>
    {
        public CreateFacilityCommandValidator()
        {

            RuleFor(x => x.Name).NotEmpty().WithMessage("Facility Name Required").WithState(_ => ErrorCode.FacilityNameRequired)
                .MaximumLength(50).WithMessage("Facility Name is too Long").WithState(_ => ErrorCode.FacilityNameTooLong);

            RuleFor(x => x.Description).NotEmpty().WithMessage("Facility Description Required").WithState(_ => ErrorCode.FacilityDescriptionRequired)
                .MaximumLength(500).WithMessage("Facility Description is too Long").WithState(_ => ErrorCode.FacilityDescriptionTooLong);

            RuleFor(x => x.Code).NotEmpty().WithMessage("Facility Code Required").WithState(_ => ErrorCode.FacilityCodeRequired)
                .MaximumLength(50).WithMessage("Facility Code is too Long").WithState(_ => ErrorCode.FacilityCodeTooLong);
        }

    }
}
