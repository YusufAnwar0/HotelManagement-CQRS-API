using Domain.Enums;
using Domain.IRepositories;
using Domain.Models;
using FluentValidation;

namespace Application.Features.Facilities.Queries.GetFacilityById
{
    public class GetFacilityByIdQueryValidator : AbstractValidator<GetFacilityByIdQuery>
    {
        public GetFacilityByIdQueryValidator()
        {
            RuleFor(x => x.id).NotEmpty().WithMessage("Facility Id Can't be empty").WithState(_ => ErrorCode.FacilityIdRequired);
        }
    }
}
