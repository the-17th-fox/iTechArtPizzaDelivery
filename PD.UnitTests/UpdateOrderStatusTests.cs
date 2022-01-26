using PD.Domain.Constants.Exceptions;
using PD.Domain.Constants.OrderStatuses;
using PD.UnitTests.TestsConfiguration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PD.UnitTests
{
    public class UpdateOrderStatusTests
    {
        // Order does not exist or it's not active anymore
        [Fact]
        public async Task UpdateOrderStatusAsync_InvalidData_InvalidOrderId()
        {
            MockConfiguration servicesConfig = new MockConfiguration();
            servicesConfig.ordersRepositoryMock.Setup(rep => 
                rep.GetByIdAsync(EntitesMocks.NonExistingOrderId).Result);

            var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
                    servicesConfig.fakeOrdersService
                        .UpdateOrderStatusAsync(EntitesMocks.NonExistingOrderId, (int)OrderStatuses.InProccesOfCreating));

            Assert.Equal("The order is not active anymore or it does not exist.", exception.Message);
        }

        // Order status does not exist (order id is valid)
        [Fact]
        public async Task UpdateOrderStatusAsync_InvalidData_InvalidStatusId()
        {
            MockConfiguration servicesConfig = new MockConfiguration();
            servicesConfig.ordersRepositoryMock.Setup(rep => 
                rep.GetByIdAsync(EntitesMocks.ActiveOrderId).Result)
                    .Returns(EntitesMocks.ActiveOrder);

            var exception = await Assert.ThrowsAsync<NotFoundException>(() => 
                    servicesConfig.fakeOrdersService
                        .UpdateOrderStatusAsync(EntitesMocks.ActiveOrderId, EntitesMocks.NonExistingOrderStatusId));

            Assert.Equal("There is no order status with the specified Id.", exception.Message);
        }

        // Order and OrderStatusId are valid
        [Fact]
        public async Task UpdateOrderStatusAsync_ValidData()
        {
            var editedOrder = EntitesMocks.ActiveOrder;
            editedOrder.OrderStatusId = EntitesMocks.ExistingOrderStatusId;

            MockConfiguration servicesConfig = new MockConfiguration();
            servicesConfig.ordersRepositoryMock.Setup(rep => 
                rep.GetByIdAsync(EntitesMocks.ActiveOrderId).Result)
                    .Returns(EntitesMocks.ActiveOrder);

            servicesConfig.ordersRepositoryMock.Setup(rep => 
                rep.UpdateOrderStatusAsync(EntitesMocks.ActiveOrder, EntitesMocks.ExistingOrderStatusId).Result)
                    .Returns(editedOrder);

            var result = await servicesConfig.fakeOrdersService
                        .UpdateOrderStatusAsync(EntitesMocks.ActiveOrderId, EntitesMocks.ExistingOrderStatusId);

            Assert.True(result.Id == EntitesMocks.ActiveOrderId);
            Assert.True(result.OrderStatus == Enum.GetName(typeof(OrderStatuses), EntitesMocks.ExistingOrderStatusId));
        }
    }
}
