using AutoMapper;
using PD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PD.Web.Models
{
    public class PromoCodesProfile : Profile
    {
        public PromoCodesProfile()
        {
            CreateMap<PromoCode, PromoCodeViewModel>() // Detailed promocode's info
                .ForMember(pc => pc.Id,
                    option => option.MapFrom(src => src.Id)) // ID

                .ForMember(pc => pc.Name,
                    option => option.MapFrom(src => src.Name)) // NAME

                .ForMember(pc => pc.Description,
                    option => option.MapFrom(src => src.Description)) // DESCRIPTION

                .ForMember(pc => pc.Orders,
                    option => option.MapFrom(src => src.Orders)) // ORDERS list
                .ReverseMap();


            CreateMap<PromoCode, ShortPromoCodeViewModel>() // Short piece of information about a promocode
                .ForMember(pc => pc.Id,
                    option => option.MapFrom(src => src.Id)) // ID

                .ForMember(pc => pc.Name,
                    option => option.MapFrom(src => src.Name)); // NAME


            CreateMap<PromoCode, AddPromoCodeModel>() // Information that's used by an AddMethod
                .ForMember(pc => pc.Name,
                    option => option.MapFrom(src => src.Name)) // NAME

                .ForMember(pc => pc.Description,
                    option => option.MapFrom(src => src.Description)) // DESCRIPTION

                .ForMember(pc => pc.DiscountAmount,
                    option => option.MapFrom(src => src.DiscountAmount)) // DISCOUNT AMOUNT
                .ReverseMap();
        }
    }
}
