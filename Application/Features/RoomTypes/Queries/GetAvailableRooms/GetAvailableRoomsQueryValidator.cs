using Domain.Enums;
using FluentValidation;

namespace Application.Features.RoomTypes.Queries.GetAvailableRooms
{
    public class GetAvailableRoomsQueryValidator : AbstractValidator<GetAvailableRoomsQuery>
    {
        public GetAvailableRoomsQueryValidator()
        {
            RuleFor(x => x.CheckInDate).NotEmpty().WithMessage("CheckInDate Can't be empty").WithState(_ => ErrorCode.ReservationCheckInDateRequired)
                .GreaterThanOrEqualTo(DateTime.UtcNow.Date).WithMessage("CheckInDate is Invalid").WithState(_ => ErrorCode.ReservationCheckInDateInvalid);

            RuleFor(x => x.CheckOutDate).NotEmpty().WithMessage("CheckOutDate Can't be empty").WithState(_ => ErrorCode.ReservationCheckOutDateRequired)
                .GreaterThan(x => x.CheckInDate).WithMessage("Check Out Must Be After Check In").WithState(_ => ErrorCode.ReservationCheckOutDateInvalid);

            RuleFor(x => x.GuestCount).NotEmpty().WithMessage("Guest Count Can't be empty").WithState(_ => ErrorCode.ValidationError);
        }
    }
}
