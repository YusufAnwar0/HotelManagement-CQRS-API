using Application.DTOs.ResponseDTOs;
using AutoMapper;
using Domain.Enums;
using Domain.IRepositories;
using Domain.Models;
using MediatR;

namespace Application.Features.Offers.Commands.UpdateOffer
{
    public class UpdateOfferCommandHandler : IRequestHandler<UpdateOfferCommand, ResponseDto<bool>>
    {
        private readonly IGeneralRepository<Offer> _offerRepository;
        private readonly IMapper _mapper;

        public UpdateOfferCommandHandler(IGeneralRepository<Offer> offerRepository, IMapper mapper)
        {
            _offerRepository = offerRepository;
            _mapper = mapper;
        }
        public async Task<ResponseDto<bool>> Handle(UpdateOfferCommand request, CancellationToken cancellationToken)
        {
            var offer = await _offerRepository.GetWithTrackingById(request.Id);
            if (offer == null)
                return ResponseDto<bool>.Fail(ErrorCode.OfferNotFound, "Offer Not Found");

            _mapper.Map(request, offer);

            await _offerRepository.SaveChanges();
            return ResponseDto<bool>.Success(true);
        }
    }
}
