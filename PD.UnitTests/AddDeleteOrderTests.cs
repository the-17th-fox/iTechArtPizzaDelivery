using PD.Domain.Constants.Exceptions;
using PD.UnitTests.TestsConfiguration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PD.UnitTests
{
    public class AddDeleteOrderTests
    {
        // The user already has an active order 
        [Fact]
        public async Task AddOrderAsync_InvalidData_AlreadyHasAnActiveOrder()
        {
            MockConfiguration servicesConfig = new MockConfiguration();
            servicesConfig.ordersRepositoryMock.Setup(rep =>
                rep.GetUsersActiveOrderAsync(EntitesMocks.UserWithAnActiveOrderId).Result).Returns(EntitesMocks.ActiveOrder);

            var exception = await Assert.ThrowsAsync<BadRequestException>(() =>
                    servicesConfig.fakeOrdersService
                        .AddAsync(EntitesMocks.UserWithAnActiveOrderId));

            Assert.Equal("The user already has an active order.", exception.Message);
        }

        // The user does not have any active order
        [Fact]
        public async Task AddOrderAsync_ValidData()
        { 
            MockConfiguration servicesConfig = new MockConfiguration();
            servicesConfig.ordersRepositoryMock.Setup(rep =>
                rep.GetUsersActiveOrderAsync(EntitesMocks.UserWithoutAnActiveOrderId).Result);

            servicesConfig.ordersRepositoryMock.Setup(rep =>
                rep.AddAsync(EntitesMocks.UserWithoutAnActiveOrderId).Result)
                    .Returns(EntitesMocks.NewlyAddedOrder);

            var result = await servicesConfig.fakeOrdersService.AddAsync(EntitesMocks.UserWithoutAnActiveOrderId);
            Assert.True(result.UserId == EntitesMocks.UserWithoutAnActiveOrderId);
        }

        // The user tries to delete the not existing order
        // The user tries to delete the order that can not be delete anymore
        [Fact]
        public async Task DeleteActiveOrderAsync_InvalidData_DoesNotHaveAnActiveOrder_OrderCanNotBeDeletedAnymore()
        {
            MockConfiguration servicesConfig = new MockConfiguration();
            servicesConfig.ordersRepositoryMock.Setup(rep =>
                rep.GetEditingReadyAsync(EntitesMocks.UserWithoutAnActiveOrderId).Result);

            var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
                    servicesConfig.fakeOrdersService
                        .DeleteActiveOrderAsync(EntitesMocks.UserWithoutAnActiveOrderId));

            Assert.Equal("The user does not have an active order or an order can not be edited anymore.", exception.Message);
        }

        // The user can delete the order
        [Fact]
        public async Task DeleteActiveOrderAsync_ValidData()
        {
            MockConfiguration servicesConfig = new MockConfiguration();
            servicesConfig.ordersRepositoryMock.Setup(rep =>
                rep.GetEditingReadyAsync(EntitesMocks.UserWithAnEditingReadyOrderId).Result)
                    .Returns(EntitesMocks.EditingReadyOrder);

            servicesConfig.ordersRepositoryMock.Setup(rep =>
                rep.DeleteAsync(EntitesMocks.EditingReadyOrder)).Verifiable();

            var result = await servicesConfig.fakeOrdersService.DeleteActiveOrderAsync(EntitesMocks.UserWithAnEditingReadyOrderId);
            servicesConfig.ordersRepositoryMock.Verify();
        }
    }
}
