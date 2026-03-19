using Application.DTOs.ResponseDTOs;
using Application.Features.Offers.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Enums;
using Domain.IRepositories;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Offers.Queries.GetOfferById
{
    public class GetOfferByIdQueryHandler : IRequestHandler<GetOfferByIdQuery, ResponseDto<GetOfferDto>>
    {
        private readonly IGeneralRepository<Offer> _offerRepository;
        private readonly IMapper _mapper;

        public GetOfferByIdQueryHandler(IGeneralRepository<Offer> offerRepository, IMapper mapper)
        {
            _offerRepository = offerRepository;
            _mapper = mapper;
        }
        public async Task<ResponseDto<GetOfferDto>> Handle(GetOfferByIdQuery request, CancellationToken cancellationToken)
        {
            var offer = await _offerRepository.GetAll().Where(o => o.Id == request.offerId)
                .ProjectTo<GetOfferDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (offer == null)
                return ResponseDto<GetOfferDto>.Fail(ErrorCode.OfferNotFound, "Offer Not Found");

            return ResponseDto<GetOfferDto>.Success(offer);

        }
    }
}
