using AutoMapper;
using PD.Domain.Entities;
using PD.Domain.Services.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Domain.Models.Profiles
{
    public class PageProfile : Profile
    {
        public PageProfile()
        {
            CreateMap<PagedList<Order>, PageViewModel<ShortOrderViewModel>>()
                .ForMember(m => m.Items, p => p.MapFrom(src => src.ToList()));

            CreateMap<PagedList<Pizza>, PageViewModel<ShortPizzaViewModel>>()
                .ForMember(m => m.Items, p => p.MapFrom(src => src.ToList()));

            CreateMap<PagedList<User>, PageViewModel<ShortUserViewModel>>()
                .ForMember(m => m.Items, p => p.MapFrom(src => src.ToList()));
        }
    }
}
