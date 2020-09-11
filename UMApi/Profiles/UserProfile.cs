using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UMApi.Dtos;
using UMApi.Models;

namespace UMApi.Profiles
{
    public class UserProfile : Profile
    {
        
        public UserProfile() {
            // from type A to type B
            CreateMap<CreateUserDto, User>();
            CreateMap<Role, CreateRoleDto>()/*.ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.RoleName))*/;
            CreateMap<User, ReadUserDto>().ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));
            CreateMap<CreateRoleDto, Role>();
        }
    }
}
