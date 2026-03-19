using Application.DTOs.ResponseDTOs;
using Domain.Enums;
using Domain.IRepositories;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Reservations.Commands.UpdateReservation
{
    public class UpdateReservationCommandHandler : IRequestHandler<UpdateReservationCommand, ResponseDto<bool>>
    {
        private readonly IGeneralRepository<Reservation> _reservationRepository;
        private readonly IGeneralRepository<RoomType> _roomTypeRepository;
        private readonly IGeneralRepository<Room> _roomRepository;

        public UpdateReservationCommandHandler(IGeneralRepository<Reservation> reservationRepository, IGeneralRepository<RoomType> roomTypeRepository, IGeneralRepository<Room> roomRepository)
        {
            _reservationRepository = reservationRepository;
            _roomTypeRepository = roomTypeRepository;
            _roomRepository = roomRepository;
        }
        public async Task<ResponseDto<bool>> Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _reservationRepository.GetAllWithTraking().Include(r => r.RoomReservations).FirstOrDefaultAsync(r => r.Id == request.id);

            if (reservation == null)
                return ResponseDto<bool>.Fail(ErrorCode.ReservationNotFound, "Reservation not found");

            var roomTypeIds = request.RoomTypesToBook.Select(r => r.RoomTypeId).ToList();

            var roomInfo = await _roomTypeRepository.GetAll().Where(rt => roomTypeIds.Contains(rt.Id))
                            .Select(rt => new
                            {
                                roomTypeId = rt.Id,
                                price = rt.BasePrice,
                                bestOffer = rt.RoomTypeOffers.Where(rto => rto.Offer.StartDate <= request.CheckInDate && rto.Offer.EndDate >= request.CheckInDate)
                                                             .OrderByDescending(rto => rto.Offer.DiscountValue)
                                                             .Select(rto => new
                                                             {
                                                                 id = rto.OfferId,
                                                                 value = rto.Offer.DiscountValue,
                                                             }).FirstOrDefault()

                            }).ToDictionaryAsync(rt => rt.roomTypeId);

            reservation.Reschedule(request.CheckInDate, request.CheckOutDate);

            foreach(var item in request.RoomTypesToBook)
            {
                var currentRoomInfo = roomInfo[item.RoomTypeId];
                var discountValue = currentRoomInfo.bestOffer != null ? currentRoomInfo.bestOffer.value : 0;
                Guid? offerId = currentRoomInfo.bestOffer?.id;

                var availableRooms = await _roomRepository.GetAll().Where(r => r.RoomTypeId == item.RoomTypeId && r.Status == RoomStatus.Available)
                                                                   .Where(r => r.RoomReservations
                                                                   .All(rr => rr.Reservation.Status == ReservationStatus.Cancelled ||
                                                                              rr.Reservation.Id == request.id ||
                                                                              rr.Reservation.CheckOutDate <= request.CheckInDate ||
                                                                              rr.Reservation.CheckInDate >= request.CheckOutDate))
                                                                   .Take(item.Quantity)
                                                                   .ToListAsync();

                if (availableRooms.Count < item.Quantity)
                    return ResponseDto<bool>.Fail(ErrorCode.RoomsSoldOut, $"Not enough rooms available for Room Type ID: {item.RoomTypeId}");

                foreach (var room in availableRooms)
                {
                    reservation.AddRoomBooking(room.Id, currentRoomInfo.price, discountValue, offerId);
                }
            }

            await _reservationRepository.SaveChanges();
            return ResponseDto<bool>.Success(true);
        }
    }
}
