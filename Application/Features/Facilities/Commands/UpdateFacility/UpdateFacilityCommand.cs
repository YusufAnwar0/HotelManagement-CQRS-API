using Application.DTOs.ResponseDTOs;
using MediatR;

namespace Application.Features.Facilities.Commands.UpdateFacility
{
    public record UpdateFacilityCommand(Guid id, string Name, string Code, string Description) : IRequest<ResponseDto<bool>>
    {
        public string Code { get; init; } = Code.ToUpperInvariant().Trim();
    };
    
}
