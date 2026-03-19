using Application.DTOs.ResponseDTOs;
using AutoMapper;
using Domain.Enums;
using Domain.IRepositories;
using Domain.Models;
using MediatR;

namespace Application.Features.Offers.Commands.CreateOffer
{
    public class CreateOfferCommandHandler : IRequestHandler<CreateOfferCommand, ResponseDto<bool>>
    {
        private readonly IGeneralRepository<Offer> _offerRepository;
        private readonly IMapper _mapper;

        public CreateOfferCommandHandler(IGeneralRepository<Offer> offerRepository, IMapper mapper)
        {
            _offerRepository = offerRepository;
            _mapper = mapper;
        }
        public async Task<ResponseDto<bool>> Handle(CreateOfferCommand request, CancellationToken cancellationToken)
        {
            var offerExist = await _offerRepository.IsExistAsync(x => x.Code == request.Code);
            if (offerExist)
                return ResponseDto<bool>.Fail(ErrorCode.OfferCodeAlreadyExists, "Offer Code Already Exists");

            var offer = _mapper.Map<Offer>(request);

            await _offerRepository.AddAsync(offer);
            await _offerRepository.SaveChanges();

            return ResponseDto<bool>.Success(true);
        }
    }
}
