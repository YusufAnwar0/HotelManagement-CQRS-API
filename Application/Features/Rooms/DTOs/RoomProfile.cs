using Application.Features.Rooms.Commands.CreateRoom;
using Application.Features.Rooms.Commands.UpdateRoom;
using AutoMapper;
using Domain.Models;

namespace Application.Features.Rooms.DTOs
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<UpdateRoomCommand, Room>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<CreateRoomCommand, Room>();

            CreateMap<Room, RoomDto>()
                .ForMember(des => des.RoomTypeName, src => src.MapFrom(r => r.RoomType.Name))
                .ForMember(des => des.BasePrice, src => src.MapFrom(r => r.RoomType.BasePrice))
                .ForMember(des => des.FacilityNames, src => src.MapFrom(r => r.RoomType.RoomTypeFacilities.Select(rtf => rtf.Facility.Name)));

        }
    }
}
