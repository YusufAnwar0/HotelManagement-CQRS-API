using Application.DTOs.ResponseDTOs;
using MediatR;

namespace Application.Features.Offers.Commands.RemoveOfferFromRoomType
{
    public record RemoveOfferFromRoomTypeCommand(Guid offerId, Guid roomTypeId) : IRequest<ResponseDto<bool>>;
    
}
