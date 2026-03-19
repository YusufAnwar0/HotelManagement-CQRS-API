using Application.Features.Offers.Commands.CreateOffer;
using Application.Features.Offers.Commands.UpdateOffer;
using AutoMapper;
using Domain.Models;

namespace Application.Features.Offers.DTOs
{
    public class OfferProfile : Profile
    {
        public OfferProfile()
        {
            CreateMap<CreateOfferCommand, Offer>();
            CreateMap<UpdateOfferCommand, Offer>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((des, src, srcMember) => srcMember != null));

            CreateMap<Offer, bestOfferDto>();
        }
    }
}
