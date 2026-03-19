using Application.Features.RoomTypes.Commands.CreateRoomType;
using Application.Features.RoomTypes.Commands.UpdateRoomType;
using AutoMapper;
using Domain.Models;

namespace Application.Features.RoomTypes.DTOs
{
    public class RoomTypeProfile : Profile
    {
        public RoomTypeProfile()
        {
            CreateMap<CreateRoomTypeCommand, RoomType>();
            CreateMap<UpdateRoomTypeCommand, RoomType>()
                .ForMember(des => des.Id, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((des, src, srcMember) => srcMember != null));

            CreateMap<RoomType, GetRoomTypeDTO>()
                .ForMember(des => des.FacilityNames, src => src.MapFrom(rt => rt.RoomTypeFacilities.Select(rtf => rtf.Facility.Name)));

            CreateMap<RoomType, RoomTypeAvailabilityDto>();
        }
    }
}
