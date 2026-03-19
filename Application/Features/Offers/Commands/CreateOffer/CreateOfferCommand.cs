using Application.DTOs.ResponseDTOs;
using MediatR;

namespace Application.Features.Offers.Commands.CreateOffer
{
    public record CreateOfferCommand(string Code, string Description, DateTime StartDate, DateTime EndDate, decimal DiscountValue) : IRequest<ResponseDto<bool>>
    {
        public string Code { get; init; } = Code.ToUpperInvariant().Trim();
    };
    
}
