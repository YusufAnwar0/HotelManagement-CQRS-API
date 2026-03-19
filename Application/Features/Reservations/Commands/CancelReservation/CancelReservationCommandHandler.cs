using Application.DTOs.ResponseDTOs;
using Domain.Enums;
using Domain.IRepositories;
using Domain.Models;
using MediatR;

namespace Application.Features.Reservations.Commands.CancelReservation
{
    public class CancelReservationCommandHandler : IRequestHandler<CancelReservationCommand, ResponseDto<bool>>
    {
        private readonly IGeneralRepository<Reservation> _reservationRepository;

        public CancelReservationCommandHandler(IGeneralRepository<Reservation> reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }
        public async Task<ResponseDto<bool>> Handle(CancelReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _reservationRepository.GetWithTrackingById(request.reservationId);
            if (reservation == null)
                return ResponseDto<bool>.Fail(ErrorCode.ReservationNotFound, "Reservation Not Found");

            reservation.Cancel();
            await _reservationRepository.SaveChanges();

            return ResponseDto<bool>.Success(true);
        }
    }
}
