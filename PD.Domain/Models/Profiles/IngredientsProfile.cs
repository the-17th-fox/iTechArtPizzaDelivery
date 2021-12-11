using AutoMapper;
using PD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PD.Domain.Models.Profiles
{
    public class IngredientsProfile : Profile
    {
        public IngredientsProfile()
        {
            CreateMap<Ingredient, IngredientViewModel>().ReverseMap();
            CreateMap<Ingredient, ShortIngredientViewModel>().ReverseMap();

            CreateMap<Ingredient, AddIngredientViewModel>().ReverseMap();
        }
    }
}
