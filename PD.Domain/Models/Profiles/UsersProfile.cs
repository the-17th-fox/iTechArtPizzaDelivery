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
            CreateMap<User, UserViewModel>();
            CreateMap<User, ShortUserViewModel>().ReverseMap();

            CreateMap<LoginUserModel, User>();

            CreateMap<RegisterUserModel, User>()
                .ForMember(u => u.UserName, m => m.MapFrom(src => src.FirstName));

            CreateMap<User, UserNamesViewModel>();
        }
    }
}
