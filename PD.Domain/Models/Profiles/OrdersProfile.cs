using PD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace PD.Domain.Models.Profiles
{
    public class OrdersProfile : Profile
    {
        public OrdersProfile()
        {
            CreateMap<Order, OrderViewModel>().ReverseMap();
            CreateMap<Order, ShortOrderViewModel>().ReverseMap();
            CreateMap<Order, OrderIdOnlyViewModel>().ReverseMap();

            CreateMap<Order, AddOrderViewModel>().ReverseMap();

            CreateMap<Order, OrderPizzasViewModel>().ReverseMap();
            CreateMap<Order, OrderPromoCodeViewModel>().ReverseMap();

            CreateMap<Order, OrderAdressViewModel>().ReverseMap();
            CreateMap<Order, OrderIsPaidStatusViewModel>().ReverseMap();
            CreateMap<Order, OrderDeliveryMethodViewModel>().ReverseMap();
            CreateMap<Order, OrderDeliveryStatusViewModel>().ReverseMap();
            CreateMap<Order, OrderDescriptionViewModel>().ReverseMap();
        }
    }
}
