using Application.DTOs.ResponseDTOs;
using MediatR;

namespace Application.Features.Offers.Commands.AddOfferToRoomType
{
    public record AddOfferToRoomTypeCommand(Guid offerId, Guid roomTypeId) : IRequest<ResponseDto<bool>>;
    
}
