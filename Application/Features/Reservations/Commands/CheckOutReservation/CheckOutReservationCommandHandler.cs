using Application.DTOs.ResponseDTOs;
using Domain.Enums;
using Domain.IRepositories;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Reservations.Commands.CheckOutReservation
{
    public class CheckOutReservationCommandHandler : IRequestHandler<CheckOutReservationCommand, ResponseDto<bool>>
    {
        private readonly IGeneralRepository<RoomReservation> _roomReservationRepository;
        private readonly IGeneralRepository<Reservation> _reservationRepository;

        public CheckOutReservationCommandHandler(IGeneralRepository<RoomReservation> roomReservationRepository, IGeneralRepository<Reservation> reservationRepository)
        {
            _roomReservationRepository = roomReservationRepository;
            _reservationRepository = reservationRepository;
        }
        public async Task<ResponseDto<bool>> Handle(CheckOutReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _reservationRepository.GetWithTrackingById(request.reservationId);
            if (reservation == null)
                return ResponseDto<bool>.Fail(ErrorCode.ReservationNotFound, "Reservation Not Found");

            reservation.CheckOut();

            var rooms = await _roomReservationRepository.GetAll()
                .Where(rr => rr.ReservationId == request.reservationId)
                .Select(rr => rr.Room)
                .ToListAsync();

            foreach (var room in rooms)
            {
                room.Status = RoomStatus.Available;
            }
            await _reservationRepository.SaveChanges();

            return ResponseDto<bool>.Success(true);
        }
    }
}
