using Application.Features.Facilities.Commands.UpdateFacility;
using AutoMapper;
using Domain.Models;

namespace Application.Features.Facilities.DTOs
{
    public class FacilityProfile : Profile
    {
        public FacilityProfile()
        {
            CreateMap<UpdateFacilityCommand, Facility>();
            CreateMap<Facility, GetFacilityDto>();
        }
    }
}
