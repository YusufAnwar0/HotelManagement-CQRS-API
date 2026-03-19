using Application.DTOs.ResponseDTOs;
using Application.Features.Facilities.DTOs;
using MediatR;

namespace Application.Features.Facilities.Queries.GetAllFacilities
{
    public record GetAllFacilitiesQuery() : IRequest<ResponseDto<IEnumerable<GetFacilityDto>>>;
    
}
