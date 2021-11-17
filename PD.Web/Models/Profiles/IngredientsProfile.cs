using AutoMapper;
using PD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PD.Web.Models
{
    public class IngredientsProfile : Profile
    {
        public IngredientsProfile()
        {
            CreateMap<Ingredient, IngredientViewModel>()
                .ForMember(i => i.Id,
                    option => option.MapFrom(src => src.Id)) // ID

                .ForMember(i => i.Name,
                    option => option.MapFrom(src => src.Name)) // NAME

                .ForMember(i => i.Pizzas,
                    option => option.MapFrom(src => src.Pizzas)); // PIZZAS list

            CreateMap<Ingredient, ShortIngredientViewModel>()
                .ForMember(i => i.Id,
                    option => option.MapFrom(src => src.Id)) // ID

                .ForMember(i => i.Name,
                    option => option.MapFrom(src => src.Name)); // NAME
        }
    }
}
