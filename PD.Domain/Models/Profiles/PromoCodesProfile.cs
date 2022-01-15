using AutoMapper;
using PD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PD.Domain.Models
{
    public class PromoCodesProfile : Profile
    {
        public PromoCodesProfile()
        {
            CreateMap<PromoCode, PromoCodeViewModel>().ReverseMap();

            CreateMap<PromoCode, ShortPromoCodeViewModel>().ReverseMap();

            CreateMap<AddPromoCodeViewModel, PromoCode>()
                .ForMember(
                    p => p.ExpirationDate, 
                    m => m.MapFrom(src => DateTime.UtcNow.Add(TimeSpan.FromHours(src.LifetimeInHours))));
        }
    }
}
