using Application.DTOs.ResponseDTOs;
using MediatR;

namespace Application.Features.Facilities.Commands.DeleteFacility
{
    public record DeleteFacilityCommand(Guid facilityId) : IRequest<ResponseDto<bool>>;
    
}
