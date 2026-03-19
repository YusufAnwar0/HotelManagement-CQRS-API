using Application.DTOs.ResponseDTOs;
using MediatR;

namespace Application.Features.Offers.Commands.UpdateOffer
{
    public record UpdateOfferCommand(Guid Id ,string? Description, DateTime? StartDate, DateTime? EndDate, decimal? DiscountValue) : IRequest<ResponseDto<bool>>;
    
}
