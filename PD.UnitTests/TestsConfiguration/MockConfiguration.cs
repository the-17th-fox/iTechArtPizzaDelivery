using AutoMapper;
using Moq;
using PD.Domain.Interfaces;
using PD.Domain.Models.Profiles;
using PD.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.UnitTests.TestsConfiguration
{
    public class MockConfiguration
    {
        public readonly OrdersService fakeOrdersService;
        public readonly MapperConfiguration mapperConfiguration;
        public readonly IMapper autoMapperMock;
        public readonly Mock<IOrdersRepository> ordersRepositoryMock;
        public readonly Mock<IPizzasRepository> pizzasRepositoryMock;
        public readonly Mock<IPromoCodesRepository> promoCodesRepositoryMock;

        public MockConfiguration()
        {
            mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new OrdersProfile());
                cfg.AddProfile(new PizzasProfile());
            });
            autoMapperMock = mapperConfiguration.CreateMapper();
            ordersRepositoryMock = new Mock<IOrdersRepository>();
            pizzasRepositoryMock = new Mock<IPizzasRepository>();
            promoCodesRepositoryMock = new Mock<IPromoCodesRepository>();

            fakeOrdersService = new OrdersService(ordersRepositoryMock.Object, pizzasRepositoryMock.Object, promoCodesRepositoryMock.Object, autoMapperMock);
        }
        
    }
}
