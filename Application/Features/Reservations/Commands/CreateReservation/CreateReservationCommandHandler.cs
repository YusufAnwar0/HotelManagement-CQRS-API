using Application.DTOs.ResponseDTOs;
using Domain.Enums;
using Domain.IRepositories;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Reservations.Commands.CreateReservation
{
    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, ResponseDto<bool>>
    {
        private readonly IGeneralRepository<RoomType> _roomTypeRepository;
        private readonly IGeneralRepository<Room> _roomRepository;
        private readonly IGeneralRepository<Reservation> _reservationRepository;

        public CreateReservationCommandHandler(IGeneralRepository<RoomType> roomTypeRepository, IGeneralRepository<Room> roomRepository, IGeneralRepository<Reservation> reservationRepository)
        {
            _roomTypeRepository = roomTypeRepository;
            _roomRepository = roomRepository;
            _reservationRepository = reservationRepository;
        }

        public async Task<ResponseDto<bool>> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = new Reservation(request.userId, request.CheckInDate, request.CheckOutDate);

            var roomTypeIds = request.RoomTypesToBook.Select(rt => rt.RoomTypeId).ToList();

            var roomInfo = await _roomTypeRepository.GetAll().Where(rt => roomTypeIds.Contains(rt.Id))
                .Select(rt => new
                {
                    roomTypeId = rt.Id,
                    name = rt.Name,
                    price = rt.BasePrice,
                    maxCapacity = rt.MaxCapacity,
                    bestOffer = rt.RoomTypeOffers.Where(rto => rto.Offer.StartDate <= request.CheckInDate && rto.Offer.EndDate >= request.CheckInDate)
                                                 .OrderByDescending(rto => rto.Offer.DiscountValue)
                                                 .Select(rto => new
                                                 {
                                                    id = rto.OfferId,
                                                    value = rto.Offer.DiscountValue,
                                                 }).FirstOrDefault()

                }).ToDictionaryAsync(rt => rt.roomTypeId);


            foreach(var item in request.RoomTypesToBook)
            {
                var currentRoomInfo = roomInfo[item.RoomTypeId];
                var discountValue = currentRoomInfo.bestOffer != null ? currentRoomInfo.bestOffer.value : 0;
                Guid? offerId = currentRoomInfo.bestOffer?.id;

                if (item.GuestsCount > (currentRoomInfo.maxCapacity * item.Quantity))
                    return ResponseDto<bool>.Fail(ErrorCode.CapacityExceeded, $"The number of guests ({item.GuestsCount}) exceeds the maximum capacity for the selected {currentRoomInfo.name} rooms");

                var availableRooms = await _roomRepository.GetAll().Where(r => r.RoomTypeId == item.RoomTypeId && r.Status != RoomStatus.UnderMaintenance)
                                                                   .Where(r => r.RoomReservations
                                                                   .All(rr => rr.Reservation.Status == ReservationStatus.Cancelled ||
                                                                              rr.Reservation.CheckOutDate <= request.CheckInDate ||
                                                                              rr.Reservation.CheckInDate >= request.CheckOutDate))
                                                                   .Take(item.Quantity)
                                                                   .ToListAsync();

                if (availableRooms.Count < item.Quantity) 
                    return ResponseDto<bool>.Fail(ErrorCode.RoomsSoldOut, $"Sorry, there are not enough available rooms of type '{currentRoomInfo.name}' for your selected dates");

                foreach(var room in availableRooms)
                {
                    reservation.AddRoomBooking(room.Id, currentRoomInfo.price, discountValue, offerId);
                }

            }
            await _reservationRepository.AddAsync(reservation);
            await _reservationRepository.SaveChanges();

            return ResponseDto<bool>.Success(true);

        }
    }
}
