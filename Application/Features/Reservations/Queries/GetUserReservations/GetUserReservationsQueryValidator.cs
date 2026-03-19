using Domain.Enums;
using FluentValidation;

namespace Application.Features.Reservations.Queries.GetUserReservations
{
    public class GetUserReservationsQueryValidator : AbstractValidator<GetUserReservationsQuery>
    {
        public GetUserReservationsQueryValidator()
        {
            RuleFor(x => x.userId).NotEmpty().WithMessage("User Id Can't be empty").WithState(_ => ErrorCode.UserRequired);
        }
    }
}
