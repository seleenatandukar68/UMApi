using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UMApi.Dtos;
using UMApi.Models;
using UMApi.Models.Menus;

namespace UMApi.Profiles
{
    public class UserProfile : Profile
    {
        
        public UserProfile() {

            // from type A to type B

            CreateMap<CreateUserDto, User>();
            CreateMap<Sub, SubDto>();
            CreateMap<Main, MainDto>();
            CreateMap<Sub, SubDto>()
                .ForMember(dest => dest.MainMenu, opt => opt.MapFrom(src => src.MainMenu));
             
            CreateMap<Role, CreateRoleDto>()
                .ForMember(dest => dest.Subs, opt => opt.MapFrom(src => src.Subs));
                
            CreateMap<User, ReadUserDto>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));
               
            CreateMap<CreateRoleDto, Role>();

            
            
              
        }
      
    }
}
