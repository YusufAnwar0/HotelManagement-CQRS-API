using Application.DTOs.ResponseDTOs;
using Application.Features.Offers.DTOs;
using MediatR;

namespace Application.Features.Offers.Queries.GetOfferById
{
    public record GetOfferByIdQuery(Guid offerId) : IRequest<ResponseDto<GetOfferDto>>;
    
}
