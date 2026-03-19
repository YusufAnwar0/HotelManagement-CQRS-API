using Domain.Enums;
using FluentValidation;

namespace Application.Features.Reservations.Commands.CheckOutReservation
{
    public class CheckOutReservationCommandValidator : AbstractValidator<CheckOutReservationCommand>
    {
        public CheckOutReservationCommandValidator()
        {
            RuleFor(x => x.reservationId).NotEmpty().WithMessage("Reservation Id Can't be empty").WithState(_ => ErrorCode.ReservationIdRequired);
        }
    }
}
