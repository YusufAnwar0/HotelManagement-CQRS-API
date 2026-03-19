using Application.DTOs.ResponseDTOs;
using Application.Features.Offers.DTOs;
using Application.Features.RoomTypes.DTOs;
using AutoMapper;
using Domain.Enums;
using Domain.IRepositories;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.RoomTypes.Queries.GetAvailableRooms
{
    public class GetAvailableRoomsQueryHandler : IRequestHandler<GetAvailableRoomsQuery, ResponseDto<IEnumerable<RoomTypeAvailabilityDto>>>
    {
        private readonly IGeneralRepository<RoomType> _roomTypeRepository;
        private readonly IMapper _mapper;

        public GetAvailableRoomsQueryHandler(IGeneralRepository<RoomType> roomTypeRepository, IMapper mapper)
        {
            _roomTypeRepository = roomTypeRepository;
            _mapper = mapper;
        }
        public async Task<ResponseDto<IEnumerable<RoomTypeAvailabilityDto>>> Handle(GetAvailableRoomsQuery request, CancellationToken cancellationToken)
        {
            var availableRooms = await _roomTypeRepository.GetAll().Where(rt => rt.MaxCapacity >= request.GuestCount)
                                                             .Where(rt => rt.Rooms.Any(r => r.RoomReservations
                                                               .All(rr =>
                                                                    rr.Reservation.Status == ReservationStatus.Cancelled ||
                                                                    rr.Reservation.CheckOutDate <= request.CheckInDate ||
                                                                    rr.Reservation.CheckInDate >= request.CheckOutDate)))
                                                             .Select(rt => new RoomTypeAvailabilityDto
                                                             {
                                                                 id = rt.Id,
                                                                 Name = rt.Name,
                                                                 Description = rt.Description,
                                                                 BasePrice = rt.BasePrice,
                                                                 MaxCapacity = rt.MaxCapacity,
                                                                 bestOffer = rt.RoomTypeOffers
                                                                               .Where(rto => rto.Offer.StartDate <= request.CheckInDate &&
                                                                                             rto.Offer.EndDate >= request.CheckInDate)
                                                                               .OrderByDescending(rto => rto.Offer.DiscountValue)
                                                                               .Select(rto => new bestOfferDto
                                                                               {
                                                                                   Id = rto.OfferId,
                                                                                   Code = rto.Offer.Code,
                                                                                   DiscountValue = rto.Offer.DiscountValue
                                                                               })
                                                                               .FirstOrDefault()
                                                             }).ToListAsync();


            return ResponseDto<IEnumerable<RoomTypeAvailabilityDto>>.Success(availableRooms);
        }
    }
}
