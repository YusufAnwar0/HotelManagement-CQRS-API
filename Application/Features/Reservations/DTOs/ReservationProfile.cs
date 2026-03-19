using AutoMapper;
using Domain.Models;

namespace Application.Features.Reservations.DTOs
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<Reservation, ReservationDetailsDto>()
                .ForMember(des => des.Rooms, opt => opt.MapFrom(src => src.RoomReservations));

            CreateMap<RoomReservation, RoomReservationItemDto>()
                .ForMember(des => des.RoomNumber, opt => opt.MapFrom(src => src.Room.RoomNumber));

            CreateMap<Reservation, ReservationSummaryDto>();
        }
    }
}
