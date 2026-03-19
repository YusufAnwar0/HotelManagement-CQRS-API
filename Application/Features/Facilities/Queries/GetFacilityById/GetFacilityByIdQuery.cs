using Application.DTOs.ResponseDTOs;
using Application.Features.Facilities.DTOs;
using MediatR;

namespace Application.Features.Facilities.Queries.GetFacilityById
{
    public record GetFacilityByIdQuery (Guid id) : IRequest<ResponseDto<GetFacilityDto>>;
    
}
