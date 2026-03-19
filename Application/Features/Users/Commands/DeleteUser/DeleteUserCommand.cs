using Application.DTOs.ResponseDTOs;
using MediatR;

namespace Application.Features.Users.Commands.DeleteUser
{
    public record DeleteUserCommand(Guid userId) : IRequest<ResponseDto<bool>>;
    
}
