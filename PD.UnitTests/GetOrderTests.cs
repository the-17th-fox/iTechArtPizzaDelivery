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
    public class GetOrderTests
    {
        [Fact]
        public async Task GetActiveOrderAsync_InvalidData_UserDoesNotHaveAnyActiveOrder()
        {
            MockConfiguration servicesConfig = new MockConfiguration();
            servicesConfig.ordersRepositoryMock.Setup(rep =>
                rep.GetActiveOrderAsync(EntitesMocks.UserWithoutAnActiveOrderId).Result);

            var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
                    servicesConfig.fakeOrdersService
                        .GetUsersActiveOrderAsync(EntitesMocks.UserWithoutAnActiveOrderId));

            Assert.Equal("The order is not active anymore or it does not exist.", exception.Message);
        }

        [Fact]
        public async Task GetActiveOrderAsync_ValidData()
        {
            MockConfiguration servicesConfig = new MockConfiguration();
            servicesConfig.ordersRepositoryMock.Setup(rep =>
                rep.GetActiveOrderAsync(EntitesMocks.UserWithAnActiveOrderId).Result).Returns(EntitesMocks.ActiveOrder);

            var result = await servicesConfig.fakeOrdersService.GetUsersActiveOrderAsync(EntitesMocks.UserWithAnActiveOrderId);

            Assert.True(result.UserId == EntitesMocks.UserWithAnActiveOrderId);
            Assert.True(result.IsActive == true);
        }

    }
}
