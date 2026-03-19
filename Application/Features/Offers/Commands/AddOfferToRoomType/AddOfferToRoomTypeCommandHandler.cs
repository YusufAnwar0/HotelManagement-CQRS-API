using Application.DTOs.ResponseDTOs;
using Domain.Enums;
using Domain.IRepositories;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Offers.Commands.AddOfferToRoomType
{
    public class AddOfferToRoomTypeCommandHandler : IRequestHandler<AddOfferToRoomTypeCommand, ResponseDto<bool>>
    {
        private readonly IGeneralRepository<Offer> _offerRepository;
        private readonly IGeneralRepository<RoomType> _roomTypeRepository;
        private readonly IGeneralRepository<RoomTypeOffer> _roomTypeOfferRepository;

        public AddOfferToRoomTypeCommandHandler(IGeneralRepository<Offer> offerRepository, IGeneralRepository<RoomType> roomTypeRepository, IGeneralRepository<RoomTypeOffer> roomTypeOfferRepository)
        {
            _offerRepository = offerRepository;
            _roomTypeRepository = roomTypeRepository;
            _roomTypeOfferRepository = roomTypeOfferRepository;
        }
        public async Task<ResponseDto<bool>> Handle(AddOfferToRoomTypeCommand request, CancellationToken cancellationToken)
        {
            var offerExist = await _offerRepository.IsExistAsync(o => o.Id == request.offerId);
            if (!offerExist)
                return ResponseDto<bool>.Fail(ErrorCode.OfferNotFound, "Offer Not Found");

            var roomTypeInfo = await _roomTypeRepository.GetAll()
                .Where(rt => rt.Id == request.roomTypeId)
                .Select(rt => new
                        {
                            offerAssigned = rt.RoomTypeOffers.Any(rto => rto.OfferId == request.offerId),
                        })
                .FirstOrDefaultAsync();

            if(roomTypeInfo == null)
                return ResponseDto<bool>.Fail(ErrorCode.RoomTypeNotFound, "Room Type Not Found");

            if(roomTypeInfo.offerAssigned)
                return ResponseDto<bool>.Fail(ErrorCode.DuplicateEntry, "Offer Already Assigned To RoomType");

            var roomTypeOffer = new RoomTypeOffer { RoomTypeId = request.roomTypeId, OfferId = request.offerId };

            await _roomTypeOfferRepository.AddAsync(roomTypeOffer);
            await _roomTypeOfferRepository.SaveChanges();

            return ResponseDto<bool>.Success(true);

        }
    }
}
