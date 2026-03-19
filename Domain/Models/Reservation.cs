using Domain.Enums;

namespace Domain.Models
{
    public class Reservation : BaseModel
    {
        public DateTime CheckInDate { get; private set; }
        public DateTime CheckOutDate { get; private set; }
        public ReservationStatus Status { get; private set; }
        public decimal TotalPrice { get; private set; }
        public short NumberOfNights => (short)(CheckOutDate.Date - CheckInDate.Date).Days;
        public decimal TaxRate { get; private set; } = 0.14m;


        private readonly List<RoomReservation> _roomReservations = new();
        public IReadOnlyCollection<RoomReservation> RoomReservations => _roomReservations.AsReadOnly();

        public Guid UserId { get; private set; }
        public User User { get; private set; }

        public Reservation(){}

        public Reservation(Guid userId, DateTime checkInDate, DateTime checkOutDate)
        {
            if (checkInDate > checkOutDate)
                throw new ArgumentException("Check -out date must be after check -in date.");
            
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            Status = ReservationStatus.Pending;
            UserId = userId;
        }

        public void AddRoomBooking(Guid roomId, decimal price, decimal discountValue, Guid? offerId)
        {
            var roomReservation = new RoomReservation(roomId, price, discountValue, NumberOfNights, offerId, TaxRate);
            _roomReservations.Add(roomReservation);
            RecalculateTotalPrice();
        }

        public void Reschedule(DateTime newCheckInDate, DateTime newCheckOutDate)
        {
            if(Status == ReservationStatus.Cancelled || Status == ReservationStatus.Completed)
                throw new InvalidOperationException("Cannot reschedule a cancelled or completed reservation.");

            if (newCheckInDate > newCheckOutDate)
                throw new ArgumentException("New Check-out date must be after check-in date.");

            CheckInDate = newCheckInDate;
            CheckOutDate = newCheckOutDate;

            _roomReservations.Clear();
            RecalculateTotalPrice();
        }

        private void RecalculateTotalPrice()
        {
            TotalPrice = _roomReservations.Sum(rr => rr.TotalPrice);
        }

        public void Cancel()
        {
            Status = ReservationStatus.Cancelled;
        }

        public void CheckIn()
        {
            Status = ReservationStatus.CheckedIn;
        }

        public void CheckOut()
        {
            Status = ReservationStatus.Completed;
        }

    }
}