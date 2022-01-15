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
            CreateMap<Pizza, PizzaInOrderViewModel>()
                .ForMember(
                    m => m.Amount, 
                    p => p.MapFrom(src => 
                        src.PizzaInOrders.Find(po => 
                            po.PizzaId == src.Id).Amount
                    )
                )
                .ForMember(
                    m => m.PricePerUnit,
                    p => p.MapFrom(src => src.Price)
                );
        }
    }
}
