using Application.DTOs.ResponseDTOs;
using MediatR;

namespace Application.Features.RoomTypes.Commands.UpdateRoomType
{
    public record UpdateRoomTypeCommand(Guid Id, string? Code, string? Name, string? Description, decimal? BasePrice, int? MaxCapacity) : IRequest<ResponseDto<bool>>
    {
        public string Code { get; init; } = Code?.ToUpperInvariant().Trim();
    };
    
}
