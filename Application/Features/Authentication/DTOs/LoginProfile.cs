using AutoMapper;
using Domain.Models;

namespace Application.Features.Authentication.DTOs
{
    public class LoginProfile : Profile
    {
        public LoginProfile()
        {
            CreateMap<User, UserCredentialsDto>().ForMember(u => u.RoleIds, des => des.MapFrom(src => src.UserRoles.Select(ur => ur.RoleId).ToList()));
        }
    }
}
