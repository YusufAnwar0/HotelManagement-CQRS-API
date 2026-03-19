using Application.DTOs.ResponseDTOs;
using Application.Features.Offers.DTOs;
using MediatR;

namespace Application.Features.Offers.Queries.GetAllOffers
{
    public record GetAllOffersQuery() :IRequest<ResponseDto<IEnumerable<GetOfferDto>>>;
    
}
