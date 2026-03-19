using Application.DTOs.ResponseDTOs;
using MediatR;

namespace Application.Features.Offers.Commands.DeleteOffer
{
    public record DeleteOfferCommand(Guid id) : IRequest<ResponseDto<bool>>;
    
}
