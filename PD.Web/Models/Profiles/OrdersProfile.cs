using PD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace PD.Web.Models.Profiles
{
    public class OrdersProfile : Profile
    {
        public OrdersProfile()
        {
            CreateMap<Order, OrderViewModel>() // Detailed pizza's info
                .ForMember(o => o.Id,
                    option => option.MapFrom(src => src.Id)) // ID

                .ForMember(o => o.Pizzas,
                    option => option.MapFrom(src => src.Pizzas)) // PIZZAS list

                .ForMember(o => o.Adress,
                    option => option.MapFrom(src => src.Adress)) // ADRESS

                .ForMember(o => o.IsPaid,
                    option => option.MapFrom(src => src.IsPaid)) // IS PAID bool

                .ForMember(o => o.UserId,
                    option => option.MapFrom(src => src.UserId)) // USER ID

                .ForMember(o => o.PromoCodeId,
                    option => option.MapFrom(src => src.PromoCodeId)) // PROMO CODE ID
                .ReverseMap();


            CreateMap<Order, ShortOrderViewModel>() // Short piece of information about a ingredient
                    .ForMember(o => o.Id,
                        option => option.MapFrom(src => src.Id)) // ID

                    .ForMember(o => o.UserId,
                        option => option.MapFrom(src => src.UserId)) // USER ID

                    .ForMember(o => o.IsPaid,
                        option => option.MapFrom(src => src.IsPaid)) // IS PAID bool
                    .ReverseMap();


            CreateMap<Order, AddOrderViewModel>() // Information that's used by AddMethod
                .ForMember(o => o.UserId,
                    option => option.MapFrom(src => src.UserId)) // NAME
                .ReverseMap();
            

            CreateMap<Order, PizzasInOrderViewModel>() // Information that's must be returned after adding(removing) pizza to the order
                .ForMember(o => o.Id,
                    option => option.MapFrom(src => src.Id)) // ID
                .ForMember(o => o.Pizzas,
                    option => option.MapFrom(src => src.Pizzas)) // PIZZAS list
                .ReverseMap();


            CreateMap<Order, AdressInOrderViewModel>() // Information that's must be returned after adding(removing) adress to the order
                .ForMember(o => o.Id,
                    option => option.MapFrom(src => src.Id)) // ID
                .ForMember(o => o.Adress,
                    option => option.MapFrom(src => src.Adress)) // ADRESS
                .ReverseMap();


            CreateMap<Order, PromoCodeInOrderViewModel>() // Information that's must be returned after adding(removing) promocode to the order
                .ForMember(o => o.Id,
                    option => option.MapFrom(src => src.Id)) // ID
                .ForMember(o => o.PromoCodeId,
                    option => option.MapFrom(src => src.PromoCodeId)) // PROMOCODE ID
                .ReverseMap();
        }
    }
}
