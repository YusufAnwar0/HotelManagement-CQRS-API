using Application.DTOs.ResponseDTOs;
using Domain.Enums;
using Domain.IRepositories;
using Domain.Models;
using MediatR;

namespace Application.Features.Reservations.Commands.DeleteReservation
{
    public class DeleteReservationCommandHandler : IRequestHandler<DeleteReservationCommand, ResponseDto<bool>>
    {
        private readonly IGeneralRepository<Reservation> _reservationRepository;

        public DeleteReservationCommandHandler(IGeneralRepository<Reservation> reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }
        public async Task<ResponseDto<bool>> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
        {
            var reservationExist = await _reservationRepository.IsExistAsync(r => r.Id == request.reservationId);
            if (!reservationExist)
                return ResponseDto<bool>.Fail(ErrorCode.ReservationNotFound, "Reservation Not Found");

            _reservationRepository.Delete(request.reservationId);
            await _reservationRepository.SaveChanges();

            return ResponseDto<bool>.Success(true);
        }
    }
}
