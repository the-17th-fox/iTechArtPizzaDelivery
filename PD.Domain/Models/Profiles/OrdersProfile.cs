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
                .ForMember(
                    model => model.DiscountAmount,
                    order => order.MapFrom(src =>
                        src.PromoCode.DiscountAmount
                    )
                );

            CreateMap<Order, ShortOrderViewModel>().ReverseMap();
            CreateMap<Order, OrderIdOnlyViewModel>().ReverseMap();

            CreateMap<Order, OrderPizzasViewModel>().ReverseMap();
            CreateMap<Order, OrderPromoCodeViewModel>()
                .ForMember(
                    model => model.OrderId,
                    order => order.MapFrom(src => src.Id))
                .ForMember(
                    model => model.DiscountAmount,
                    order => order.MapFrom(src => src.PromoCode.DiscountAmount));

            CreateMap<Order, OrderAdressViewModel>().ReverseMap();
            CreateMap<Order, OrderIsActiveStatusViewModel>();                
            CreateMap<Order, OrderDeliveryMethodViewModel>()
                .ForMember(
                    model => model.DeliveryMethod,
                    order => order.MapFrom(src =>
                        Enum.GetName(typeof(DeliveryMethods), src.DeliveryMethodId)
                        )
                );

            CreateMap<Order, OrderStatusViewModel>()
                .ForMember(
                    model => model.OrderStatus,
                    order => order.MapFrom(src =>
                        Enum.GetName(typeof(OrderStatuses), src.OrderStatusId)
                        )
                );
            CreateMap<Order, OrderDescriptionViewModel>().ReverseMap();
        }
    }
}
