using Application.DTOs.ResponseDTOs;
using Domain.Models;
using MediatR;

namespace Application.Features.Users.Queries.GetUserByPhone
{
    public record GetUserByPhoneQuery(string phoneNumber) : IRequest<ResponseDto<User>>;
    
}
