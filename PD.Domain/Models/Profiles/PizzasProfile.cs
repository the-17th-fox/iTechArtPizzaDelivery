using PD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace PD.Domain.Models.Profiles
{
    public class PizzasProfile : Profile
    {
        public PizzasProfile()
        {
            CreateMap<Pizza, PizzaViewModel>().ReverseMap();
            CreateMap<Pizza, ShortPizzaViewModel>().ReverseMap();

            CreateMap<Pizza, AddPizzaViewModel>().ReverseMap();

            CreateMap<Pizza, PizzaIngredientsViewModel>().ReverseMap();
            CreateMap<Pizza, PizzaDescriptionViewModel>().ReverseMap();
        }
    }
}
