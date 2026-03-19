using Domain.Enums;
using FluentValidation;

namespace Application.Features.Reservations.Commands.UpdateReservation
{
    public class UpdateReservationCommandValidator : AbstractValidator<UpdateReservationCommand>
    {
        public UpdateReservationCommandValidator()
        {
            RuleFor(x => x.id).NotEmpty().WithMessage("Reservation Id Can't be empty").WithState(_ => ErrorCode.ReservationIdRequired);

            RuleFor(x => x.CheckInDate).NotEmpty().WithMessage("CheckInDate Can't be empty").WithState(_ => ErrorCode.ReservationCheckInDateRequired)
                .GreaterThanOrEqualTo(DateTime.UtcNow.Date).WithMessage("CheckInDate is Invalid").WithState(_ => ErrorCode.ReservationCheckInDateInvalid);

            RuleFor(x => x.CheckOutDate).NotEmpty().WithMessage("CheckOutDate Can't be empty").WithState(_ => ErrorCode.ReservationCheckOutDateRequired)
                .GreaterThan(x => x.CheckInDate).WithMessage("Check Out Must Be After Check In").WithState(_ => ErrorCode.ReservationCheckOutDateInvalid);

            RuleFor(x => x.RoomTypesToBook).NotEmpty().WithMessage("Room Type To Book Can't be empty").WithState(_ => ErrorCode.ReservationRoomTypeBookingRequired);
        }
    }
}
