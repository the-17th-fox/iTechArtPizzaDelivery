using PD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace PD.Web.Models
{
    public class PizzasProfile : Profile
    {
        public PizzasProfile()
        {
            CreateMap<Pizza, PizzaViewModel>() // Detailed pizza's info
                .ForMember(p => p.Id,                           
                    option => option.MapFrom(src => src.Id)) // ID

                .ForMember(p => p.Name,
                    option => option.MapFrom(src => src.Name)) // NAME

                .ForMember(p => p.Description,
                    option => option.MapFrom(src => src.Description)) // DESCRIPTION

                .ForMember(p => p.Ingredients,
                    option => option.MapFrom(src => src.Ingredients)) // INGREDIENTS list
                .ReverseMap();


            CreateMap<Pizza, ShortPizzaViewModel>() // Short piece of information about a pizza
                .ForMember(p => p.Id,
                    option => option.MapFrom(src => src.Id)) // ID

                .ForMember(p => p.Name,
                    option => option.MapFrom(src => src.Name)) // NAME
                .ReverseMap();


            CreateMap<Pizza, AddPizzaViewModel>() // Information that's used by AddMethod
                .ForMember(p => p.Name,
                    option => option.MapFrom(src => src.Name)) // NAME

                .ForMember(p => p.Description,
                    option => option.MapFrom(src => src.Description)) // DESCRIPTION
                .ReverseMap();
        }
    }
}
