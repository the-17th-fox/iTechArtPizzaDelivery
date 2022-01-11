using PD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PD.Domain.Constants.OrderStatuses;
using PD.Domain.Constants.DeliveryMethods;

namespace PD.Domain.Models.Profiles
{
    public class OrdersProfile : Profile
    {
        public OrdersProfile()
        {
            CreateMap<Order, OrderViewModel>()
                .ForMember(
                    model => model.OrderStatus, 
                    order => order.MapFrom(src => 
                        Enum.GetName(typeof(OrderStatuses), src.OrderStatusId)
                        )
                )
                .ForMember(
                    model => model.DeliveryMethod,
                    order => order.MapFrom(src =>
                        Enum.GetName(typeof(DeliveryMethods), src.DeliveryMethodId)
                        )
                )
                .ReverseMap();
            CreateMap<Order, ShortOrderViewModel>().ReverseMap();
            CreateMap<Order, OrderIdOnlyViewModel>().ReverseMap();

            CreateMap<Order, OrderPizzasViewModel>().ReverseMap();
            CreateMap<Order, OrderPromoCodeViewModel>().ReverseMap();

            CreateMap<Order, OrderAdressViewModel>().ReverseMap();
            CreateMap<Order, OrderIsActiveStatusViewModel>().ReverseMap();
            CreateMap<Order, OrderDeliveryMethodViewModel>().ReverseMap();
            CreateMap<Order, OrderDeliveryStatusViewModel>().ReverseMap();
            CreateMap<Order, OrderDescriptionViewModel>().ReverseMap();
        }
    }
}
