using Moq;
using PD.Domain.Constants.Exceptions;
using PD.Domain.Constants.OrderStatuses;
using PD.Domain.Entities;
using PD.UnitTests.TestsConfiguration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PD.UnitTests
{
    public class AddPizzaToOrderTests
    {
        [Fact]
        public async Task AddPizzaAsync_ValidData_OrderDoesNotHaveAnyPizzaOfThisType()
        {
            MockConfiguration servicesConfig = new MockConfiguration();
            servicesConfig.ordersRepositoryMock.Setup(rep =>
                rep.GetEditingReadyAsync(EntitesMocks.UserWithAnEditingReadyOrderId).Result)
                    .Returns(EntitesMocks.EditingReadyOrder);

            servicesConfig.pizzasRepositoryMock.Setup(rep =>
                rep.GetByIdAsync(EntitesMocks.ExistingPizzaId).Result)
                    .Returns(EntitesMocks.ExistingPizza);

            servicesConfig.ordersRepositoryMock.Setup(rep =>
                rep.AddNotIncludedPizzaAsync(EntitesMocks.EditingReadyOrder, EntitesMocks.ExistingPizza, 2).Result)
                    .Returns(EntitesMocks.OrderWithPizzas).Verifiable();

            var result = await servicesConfig.fakeOrdersService
                .AddPizzaAsync(EntitesMocks.UserWithAnEditingReadyOrderId, EntitesMocks.ExistingPizzaId, EntitesMocks.PizzasInOrderAmount);

            servicesConfig.ordersRepositoryMock.Verify();
            Assert.True(result != null);
            Assert.True(result.Id == EntitesMocks.EditingReadyOrderId);
            Assert.True(result.Pizzas.Find(p =>
                p.Id == EntitesMocks.ExistingPizzaId)
                    .Amount == EntitesMocks.PizzasInOrderAmount);
        }

        [Fact]
        public async Task AddPizzaAsync_ValidData_OrderAlreadyHasAtLeastOnePizzaOfThisType()
        {
            MockConfiguration servicesConfig = new MockConfiguration();
            servicesConfig.ordersRepositoryMock.Setup(rep =>
                rep.GetEditingReadyAsync(EntitesMocks.UserWithAnEditingReadyOrderId).Result)
                    .Returns(EntitesMocks.OrderWithPizzas);

            servicesConfig.pizzasRepositoryMock.Setup(rep =>
                rep.GetByIdAsync(EntitesMocks.ExistingPizzaId).Result)
                    .Returns(EntitesMocks.PizzaInOrder);

            servicesConfig.ordersRepositoryMock.Setup(rep =>
                rep.AddIncludedPizza(EntitesMocks.OrderWithPizzas, EntitesMocks.PizzaInOrder, 2).Result)
                    .Returns(EntitesMocks.OrderWithDoubledPizzas).Verifiable();

            var result = await servicesConfig.fakeOrdersService
                .AddPizzaAsync(EntitesMocks.UserWithAnEditingReadyOrderId, EntitesMocks.ExistingPizzaId, 2);
            
            servicesConfig.ordersRepositoryMock.Verify();
            Assert.True(result != null);
            Assert.True(result.Id == EntitesMocks.EditingReadyOrderId);
            Assert.True(result.Pizzas.Find(p =>
                p.Id == EntitesMocks.ExistingPizzaId)
                    .Amount == EntitesMocks.DoubledPizzasInOrderAmount);
        }
    }
}

