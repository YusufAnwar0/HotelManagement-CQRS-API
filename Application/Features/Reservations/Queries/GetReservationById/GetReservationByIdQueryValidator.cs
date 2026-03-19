using Domain.Enums;
using FluentValidation;

namespace Application.Features.Reservations.Queries.GetReservationById
{
    public class GetReservationByIdQueryValidator : AbstractValidator<GetReservationByIdQuery>
    {
        public GetReservationByIdQueryValidator()
        {
            RuleFor(x => x.id).NotEmpty().WithMessage("Reservation Id Can't be empty").WithState(_ => ErrorCode.ReservationIdRequired);
        }
    }
}
