using Application.DTOs.ResponseDTOs;
using Domain.Enums;
using Domain.IRepositories;
using Domain.Models;
using MediatR;

namespace Application.Features.Offers.Commands.DeleteOffer
{
    public class DeleteOfferCommandHandler : IRequestHandler<DeleteOfferCommand, ResponseDto<bool>>
    {
        private readonly IGeneralRepository<Offer> _offerRepository;

        public DeleteOfferCommandHandler(IGeneralRepository<Offer> offerRepository)
        {
            _offerRepository = offerRepository;
        }
        public async Task<ResponseDto<bool>> Handle(DeleteOfferCommand request, CancellationToken cancellationToken)
        {
            var offerExist = await _offerRepository.IsExistAsync(o => o.Id == request.id);
            if (!offerExist)
                return ResponseDto<bool>.Fail(ErrorCode.OfferNotFound, "Offer Not Found");

            _offerRepository.Delete(request.id);
            await _offerRepository.SaveChanges();

            return ResponseDto<bool>.Success(true);

        }
    }
}
