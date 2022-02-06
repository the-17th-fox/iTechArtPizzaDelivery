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
    public class RemovePizzaFromOrderTests
    {
        [Fact]
        public async Task RemovePizzaAsync_InvalidData_OrderDoesNotContainSpecifiedPizza()
        {
            MockConfiguration servicesConfig = new MockConfiguration();
            servicesConfig.ordersRepositoryMock.Setup(rep =>
                rep.GetEditingReadyAsync(EntitesMocks.UserWithAnEditingReadyOrderId).Result)
                    .Returns(EntitesMocks.EditingReadyOrder);

            servicesConfig.pizzasRepositoryMock.Setup(rep =>
                rep.GetByIdAsync(EntitesMocks.ExistingPizzaId).Result)
                    .Returns(EntitesMocks.ExistingPizza);

            var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
                    servicesConfig.fakeOrdersService
                        .RemovePizzaAsync(EntitesMocks.UserWithAnEditingReadyOrderId, EntitesMocks.ExistingPizzaId, 2));

            Assert.Equal("The specified order does not contain the specified pizza.", exception.Message);
        }

        [Fact]
        public async Task RemovePizzaAsync_InvalidData_SpecifiedDeleteNumberGreaterThatPizzasNumberInOrder()
        {
            MockConfiguration servicesConfig = new MockConfiguration();
            servicesConfig.ordersRepositoryMock.Setup(rep =>
                rep.GetEditingReadyAsync(EntitesMocks.UserWithAnEditingReadyOrderId).Result)
                    .Returns(EntitesMocks.OrderWithPizzas);

            servicesConfig.pizzasRepositoryMock.Setup(rep =>
                rep.GetByIdAsync(EntitesMocks.PizzaInOrder.Id).Result)
                    .Returns(EntitesMocks.PizzaInOrder);

            var exception = await Assert.ThrowsAsync<BadRequestException>(() =>
                    servicesConfig.fakeOrdersService
                        .RemovePizzaAsync(EntitesMocks.UserWithAnEditingReadyOrderId, EntitesMocks.PizzaInOrder.Id, 4));

            Assert.Equal("The number of pizzas to remove is greater than pizzas amount in the order.", exception.Message);
        }

        // Line 66-68 returns null, test does not work properly
        [Fact]
        public async Task RemovePizzaAsync_ValidData()
        {
            MockConfiguration servicesConfig = new MockConfiguration();
            servicesConfig.ordersRepositoryMock.Setup(rep =>
                rep.GetEditingReadyAsync(EntitesMocks.UserWithAnEditingReadyOrderId).Result)
                    .Returns(EntitesMocks.OrderWithDoubledPizzas);

            servicesConfig.pizzasRepositoryMock.Setup(rep =>
                rep.GetByIdAsync(EntitesMocks.ExistingPizzaId).Result)
                    .Returns(EntitesMocks.DoubledPizzaInOrder);

            servicesConfig.ordersRepositoryMock.Setup(rep =>
                rep.RemovePizzaAsync(It.IsAny<Order>(), It.IsAny<Pizza>(), It.Is<int>(num => num == 2)).Result)
                    .Returns(EntitesMocks.OrderWithPizzas).Verifiable();

            var result = await servicesConfig.fakeOrdersService
                .RemovePizzaAsync(EntitesMocks.UserWithAnEditingReadyOrderId, EntitesMocks.ExistingPizzaId, 2);

            servicesConfig.ordersRepositoryMock.Verify();
            Assert.True(result != null);
            Assert.True(result.Id == EntitesMocks.EditingReadyOrderId);
            Assert.True(result.Pizzas.Find(p =>
                p.Id == EntitesMocks.ExistingPizzaId).Amount == 2);
        }
    }
}
