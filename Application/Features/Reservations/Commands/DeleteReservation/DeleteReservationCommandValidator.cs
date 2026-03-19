using Domain.Enums;
using FluentValidation;

namespace Application.Features.Reservations.Commands.DeleteReservation
{
    public class DeleteReservationCommandValidator : AbstractValidator<DeleteReservationCommand>
    {
        public DeleteReservationCommandValidator()
        {
            RuleFor(x => x.reservationId).NotEmpty().WithMessage("Reservation Id Can't be empty").WithState(_ => ErrorCode.ReservationIdRequired);
        }

    }
}
