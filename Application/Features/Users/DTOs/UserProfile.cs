using AutoMapper;
using Domain.Models;

namespace Application.Features.Users.DTOs
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>().ForMember(des => des.RoleNames, src => src.MapFrom(u => u.UserRoles.Select(ur => ur.Role.Name).ToList()));
            CreateMap<User, UserProfileDto>()
               .ForMember(des => des.Roles, src => src.MapFrom(u => u.UserRoles.Select(ur => ur.Role.Name)));
        }
    }
}
