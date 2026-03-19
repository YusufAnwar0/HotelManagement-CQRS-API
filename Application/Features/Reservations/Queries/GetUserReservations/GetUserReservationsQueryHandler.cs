using Application.DTOs.ResponseDTOs;
using Application.Features.Reservations.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.IRepositories;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Reservations.Queries.GetUserReservations
{
    public class GetUserReservationsQueryHandler : IRequestHandler<GetUserReservationsQuery, ResponseDto<IEnumerable<ReservationSummaryDto>>>
    {
        private readonly IGeneralRepository<Reservation> _reservationRepository;
        private readonly IMapper _mapper;

        public GetUserReservationsQueryHandler(IGeneralRepository<Reservation> reservationRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }
        public async Task<ResponseDto<IEnumerable<ReservationSummaryDto>>> Handle(GetUserReservationsQuery request, CancellationToken cancellationToken)
        {
            var reservations = await _reservationRepository.GetAll().Where(r => r.UserId == request.userId)
                .ProjectTo<ReservationSummaryDto>(_mapper.ConfigurationProvider)
                .OrderByDescending(r => r.CheckInDate)
                .Skip((request.pageNumber - 1) * request.pageSize)
                .Take(request.pageSize)
                .ToListAsync();

            return ResponseDto<IEnumerable<ReservationSummaryDto>>.Success(reservations);
        }
    }
}
