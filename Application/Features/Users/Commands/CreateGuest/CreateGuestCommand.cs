using Application.DTOs.ResponseDTOs;
using MediatR;

namespace Application.Features.Users.Commands.CreateGuest
{
    public record CreateGuestCommand(string name, string phoneNumber, string country) : IRequest<ResponseDto<Guid>> ;
    
}
