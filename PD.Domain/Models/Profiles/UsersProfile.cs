using AutoMapper;
using PD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PD.Domain.Models.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<User, ShortUserViewModel>().ReverseMap();

            CreateMap<User, LoginUserModel>().ReverseMap();

            CreateMap<User, RegisterUserModel>()
                .ForMember(u => u.FirstName, (o => o.MapFrom(src => src.UserName)))
                .ReverseMap();
        }
    }
}
