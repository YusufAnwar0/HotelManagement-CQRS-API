using Domain.Enums;
using FluentValidation;

namespace Application.Features.Reservations.Commands.CancelReservation
{
    public class CancelReservationCommandValidator : AbstractValidator<CancelReservationCommand>
    {
        public CancelReservationCommandValidator()
        {
            RuleFor(x => x.reservationId).NotEmpty().WithMessage("Reservation Id Can't be empty").WithState(_ => ErrorCode.ReservationIdRequired);
        }
    }
}
