using Application.DTOs.ResponseDTOs;
using Application.Features.Offers.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.IRepositories;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Offers.Queries.GetAllOffers
{
    public class GetAllOffersQueryHandler : IRequestHandler<GetAllOffersQuery, ResponseDto<IEnumerable<GetOfferDto>>>
    {
        private readonly IGeneralRepository<Offer> _offerRepository;
        private readonly IMapper _mapper;

        public GetAllOffersQueryHandler(IGeneralRepository<Offer> offerRepository, IMapper mapper)
        {
            _offerRepository = offerRepository;
            _mapper = mapper;
        }
        public async Task<ResponseDto<IEnumerable<GetOfferDto>>> Handle(GetAllOffersQuery request, CancellationToken cancellationToken)
        {

            var offers = await _offerRepository.GetAll()
                .ProjectTo<GetOfferDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return ResponseDto<IEnumerable<GetOfferDto>>.Success(offers);
        }
    }
}
