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
    public class GetAndCheckMethodsTests
    {
        [Fact]
        public async Task GetAndCheckOrderAsync_InvalidData_InvalidOrderId()
        {
            MockConfiguration servicesConfig = new MockConfiguration();
            servicesConfig.ordersRepositoryMock.Setup(rep =>
                rep.GetByIdAsync(EntitesMocks.NonExistingOrderId).Result);

            var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
                    servicesConfig.fakeOrdersService
                        .GetByIdAsync(EntitesMocks.NonExistingOrderId));

            Assert.Equal("The order with the specified id does not exist.", exception.Message);
        }

        [Fact]
        public async Task GetAndCheckOrderAsync_ValidData()
        {
            MockConfiguration servicesConfig = new MockConfiguration();
            servicesConfig.ordersRepositoryMock.Setup(rep =>
                rep.GetByIdAsync(EntitesMocks.ActiveOrderId).Result).Returns(EntitesMocks.ActiveOrder);

            var result = await servicesConfig.fakeOrdersService.GetByIdAsync(EntitesMocks.ActiveOrderId);
            Assert.True(result != null);
        }

        [Fact]
        public async Task GetAndCheckEditingReadyOrderAsync_InvalidData_UserDoesNotHaveEditingReadyOrder()
        {
            MockConfiguration servicesConfig = new MockConfiguration();
            servicesConfig.ordersRepositoryMock.Setup(rep =>
                rep.GetEditingReadyAsync(EntitesMocks.UserWithAnActiveOrderId).Result); // Order is active, but not editing-ready

            var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
                    servicesConfig.fakeOrdersService
                        .GetAndCheckEditingReadyOrderAsync(EntitesMocks.UserWithAnActiveOrderId));

            Assert.Equal("The user does not have an active order or the order can not be edited anymore.", exception.Message);
        }

        [Fact]
        public async Task GetAndCheckEditingReadyOrderAsync_ValidData()
        {
            MockConfiguration servicesConfig = new MockConfiguration();
            servicesConfig.ordersRepositoryMock.Setup(rep =>
                rep.GetEditingReadyAsync(EntitesMocks.UserWithAnEditingReadyOrderId).Result).Returns(EntitesMocks.EditingReadyOrder);

            var result = await servicesConfig.fakeOrdersService.GetAndCheckEditingReadyOrderAsync(EntitesMocks.UserWithAnEditingReadyOrderId);
            Assert.True(result != null);
        }

        [Fact]
        public async Task GetAndCheckActiveOrderByIdAsync_InvalidData_InvalidOrderId()
        {
            MockConfiguration servicesConfig = new MockConfiguration();
            servicesConfig.ordersRepositoryMock.Setup(rep =>
                rep.GetByIdAsync(EntitesMocks.NonExistingOrderId).Result);

            var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
                    servicesConfig.fakeOrdersService
                        .GetAndCheckActiveOrderByIdAsync(EntitesMocks.NonExistingOrderId));

            Assert.Equal("The order is not active anymore or it does not exist.", exception.Message);
        }

        [Fact]
        public async Task GetAndCheckActiveOrderByIdAsync_ValidData()
        {
            MockConfiguration servicesConfig = new MockConfiguration();
            servicesConfig.ordersRepositoryMock.Setup(rep =>
                rep.GetByIdAsync(EntitesMocks.ActiveOrderId).Result).Returns(EntitesMocks.ActiveOrder);

            var result = await servicesConfig.fakeOrdersService.GetAndCheckActiveOrderByIdAsync(EntitesMocks.ActiveOrderId);
            Assert.True(result != null);
        }

        [Fact]
        public async Task GetAndCheckActiveOrderByUserId_InvalidData_UserDoesNotHaveAnyActiveOrder()
        {
            MockConfiguration servicesConfig = new MockConfiguration();
            servicesConfig.ordersRepositoryMock.Setup(rep =>
                rep.GetUsersActiveOrderAsync(EntitesMocks.UserWithoutAnActiveOrderId).Result);

            var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
                    servicesConfig.fakeOrdersService
                        .GetAndCheckActiveOrderByUserId(EntitesMocks.UserWithoutAnActiveOrderId));

            Assert.Equal("The order is not active anymore or it does not exist.", exception.Message);
        }

        [Fact]
        public async Task GetAndCheckActiveOrderByUserId_ValidData()
        {
            MockConfiguration servicesConfig = new MockConfiguration();
            servicesConfig.ordersRepositoryMock.Setup(rep =>
                rep.GetUsersActiveOrderAsync(EntitesMocks.UserWithAnActiveOrderId).Result).Returns(EntitesMocks.ActiveOrder);

            var result = await servicesConfig.fakeOrdersService.GetAndCheckActiveOrderByUserId(EntitesMocks.UserWithAnActiveOrderId);
            Assert.True(result != null);
        }

        [Fact]
        public async Task GetAndCheckPizzaAsync_InvalidData_PizzaDoesNotExist()
        {
            MockConfiguration servicesConfig = new MockConfiguration();
            servicesConfig.pizzasRepositoryMock.Setup(rep =>
                rep.GetByIdAsync(EntitesMocks.NonExistingPizzaId).Result);

            var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
                    servicesConfig.fakeOrdersService
                        .GetAndCheckPizzaAsync(EntitesMocks.NonExistingPizzaId));

            Assert.Equal("The pizza with the specified id does not exist.", exception.Message);
        }

        [Fact]
        public async Task GetAndCheckPizzaAsync_ValidData()
        {
            MockConfiguration servicesConfig = new MockConfiguration();
            servicesConfig.pizzasRepositoryMock.Setup(rep =>
                rep.GetByIdAsync(EntitesMocks.ExistingPizzaId).Result).Returns(EntitesMocks.ExistingPizza);

            var result = await servicesConfig.fakeOrdersService.GetAndCheckPizzaAsync(EntitesMocks.ExistingPizzaId);
            Assert.True(result != null);
        }
    }
}
