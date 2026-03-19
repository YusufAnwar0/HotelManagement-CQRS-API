using Domain.Enums;
using FluentValidation;

namespace Application.Features.Reservations.Commands.CheckInReservation
{
    public class CheckInReservationCommandValidator : AbstractValidator<CheckInReservationCommand>
    {
        public CheckInReservationCommandValidator()
        {
            RuleFor(x => x.reservationId).NotEmpty().WithMessage("Reservation Id Can't be empty").WithState(_ => ErrorCode.ReservationIdRequired);
        }
    }
}
