using Application.DTOs.ResponseDTOs;
using MediatR;

namespace Application.Features.Facilities.Commands.CreateFacility
{
    public record CreateFacilityCommand(string Name, string Description, string Code) : IRequest<ResponseDto<bool>>
    {
        public string Code { get; init; } = Code?.ToUpperInvariant().Trim();
    };
    
}
