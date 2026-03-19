using Application.DTOs.ResponseDTOs;
using Application.Features.Reservations.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Enums;
using Domain.IRepositories;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Reservations.Queries.GetReservationById
{
    public class GetReservationByIdQueryHandler : IRequestHandler<GetReservationByIdQuery, ResponseDto<ReservationDetailsDto>>
    {
        private readonly IGeneralRepository<Reservation> _reservationRepository;
        private readonly IMapper _mapper;

        public GetReservationByIdQueryHandler(IGeneralRepository<Reservation> reservationRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }
        public async Task<ResponseDto<ReservationDetailsDto>> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
        {
            var reservation = await _reservationRepository.GetAll().Where(r => r.Id == request.id)
                .ProjectTo<ReservationDetailsDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (reservation == null)
                return ResponseDto<ReservationDetailsDto>.Fail(ErrorCode.ReservationNotFound, "Reservation Not Found");

            return ResponseDto<ReservationDetailsDto>.Success(reservation);
        }
    }
}
