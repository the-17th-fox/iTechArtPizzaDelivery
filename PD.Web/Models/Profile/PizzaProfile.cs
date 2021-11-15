using PD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace PD.Web.Models
{
    public class PizzaProfile : Profile
    {
        public PizzaProfile()
        {
            CreateMap<Pizza, DetailPizzaViewModel>() // GetAll
                .ForMember(p => p.Id,                           
                    option => option.MapFrom(src => src.Id)) // ID
                .ForMember(p => p.Name,
                    option => option.MapFrom(src => src.Name)) // NAME
                .ForMember(p => p.Description,
                    option => option.MapFrom(src => src.Description)) // DESCRIPTION
                .ForMember(p => p.Ingredients,
                    option => option.MapFrom(src => src.Ingredients)); // INGREDIENTS list

            CreateMap<Pizza, PizzaViewModel>() // GetById
                .ForMember(p => p.Id, 
                    option => option.MapFrom(src => src.Id)) // ID
                .ForMember(p => p.Name,
                    option => option.MapFrom(src => src.Name)); // NAME
        }

    }
}
