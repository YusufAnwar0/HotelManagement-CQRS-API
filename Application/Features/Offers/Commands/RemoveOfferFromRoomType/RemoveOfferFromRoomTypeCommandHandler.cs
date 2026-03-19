using Application.DTOs.ResponseDTOs;
using Domain.Enums;
using Domain.IRepositories;
using Domain.Models;
using MediatR;

namespace Application.Features.Offers.Commands.RemoveOfferFromRoomType
{
    public class RemoveOfferFromRoomTypeCommandHandler : IRequestHandler<RemoveOfferFromRoomTypeCommand, ResponseDto<bool>>
    {
        private readonly IGeneralRepository<RoomTypeOffer> _roomTypeOfferRepository;

        public RemoveOfferFromRoomTypeCommandHandler(IGeneralRepository<RoomTypeOffer> roomTypeOfferRepository)
        {
            _roomTypeOfferRepository = roomTypeOfferRepository;
        }
        public async Task<ResponseDto<bool>> Handle(RemoveOfferFromRoomTypeCommand request, CancellationToken cancellationToken)
        {
            var roomTypeOfferId = await _roomTypeOfferRepository.GetIdAsync(rto => rto.RoomTypeId == request.roomTypeId && rto.OfferId == request.offerId);

            if (roomTypeOfferId == Guid.Empty)
                return ResponseDto<bool>.Fail(ErrorCode.OfferNotAssigned, "Offer Not Assigned To RoomType");

            _roomTypeOfferRepository.Delete(roomTypeOfferId);
            await _roomTypeOfferRepository.SaveChanges();

            return ResponseDto<bool>.Success(true);

        }
    }
}
