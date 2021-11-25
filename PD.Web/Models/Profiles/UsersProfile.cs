using AutoMapper;
using PD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PD.Web.Models.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<User, UserViewModel>() // Detailed user's info
                .ForMember(pc => pc.Id,
                    option => option.MapFrom(src => src.Id)) // ID

                .ForMember(pc => pc.FirstName,
                    option => option.MapFrom(src => src.FirstName)) // FIRST NAME

                .ForMember(pc => pc.LastName,
                    option => option.MapFrom(src => src.LastName)) // LAST NAME

                .ForMember(pc => pc.Email,
                    option => option.MapFrom(src => src.Email)) // EMAIL

                .ForMember(pc => pc.Order,
                    option => option.MapFrom(src => src.Order)) // ORDER
                .ReverseMap();


            CreateMap<User, ShortUserViewModel>() // Short user's info
                .ForMember(pc => pc.Id,
                    option => option.MapFrom(src => src.Id)) // ID

                .ForMember(pc => pc.Email,
                    option => option.MapFrom(src => src.Email)) // EMAIL

                .ForMember(pc => pc.Order,
                    option => option.MapFrom(src => src.Order)) // ORDER
                .ReverseMap();


            CreateMap<User, RegisterUserModel>() // Register model

                .ForMember(pc => pc.FirstName,
                    option => option.MapFrom(src => src.FirstName)) // FIRST NAME

                .ForMember(pc => pc.LastName,
                    option => option.MapFrom(src => src.LastName)) // LAST NAME

                .ForMember(pc => pc.Email,
                    option => option.MapFrom(src => src.NormalizedEmail)) // EMAIL

                .ForMember(pc => pc.PhoneNumber,
                    option => option.MapFrom(src => src.PhoneNumber)) // PHONE NUM
                .ReverseMap();


            CreateMap<User, LoginUserModel>() // Login model

                .ForMember(pc => pc.Email,
                    option => option.MapFrom(src => src.NormalizedEmail)) // EMAIL
                .ReverseMap();
        }
    }
}
