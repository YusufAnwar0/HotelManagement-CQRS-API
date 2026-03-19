using AutoMapper;
using Domain.Models;

namespace Application.Features.Roles.DTOs
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleDto>();
        }
    }
}
