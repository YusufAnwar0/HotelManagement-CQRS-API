using Application.DTOs.ResponseDTOs;
using Application.Features.Users.DTOs;
using MediatR;

namespace Application.Features.Users.Queries.GetUserProfile
{
    public record GetUserProfileQuery(Guid userId) : IRequest<ResponseDto<UserProfileDto>>;
    
}
