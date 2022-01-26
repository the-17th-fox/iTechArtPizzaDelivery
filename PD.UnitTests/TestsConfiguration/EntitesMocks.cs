using PD.Domain.Constants.DeliveryMethods;
using PD.Domain.Constants.OrderStatuses;
using PD.Domain.Constants.PaymentMethods;
using PD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.UnitTests.TestsConfiguration
{
    public class EntitesMocks
    {
        public const long UserWithoutAnActiveOrderId = 0L;
        public const long UserWithAnActiveOrderId = 1L;
        public const long UserWithAnEditingReadyOrderId = 2L;

        public const long NonExistingOrderId = 0L;
        public const long ActiveOrderId = 1L;
        public const long EditingReadyOrderId = 2L;

        public const int ExistingOrderStatusId = (int)OrderStatuses.Delivering;
        public const int NonExistingOrderStatusId = 999;

        public const long NonExistingPizzaId = 0L;
        public const long ExistingPizzaId = 1L;

        public const int PizzasInOrderAmount = 2;
        public const int DoubledPizzasInOrderAmount = 4;

        public static Order ActiveOrder = new Order()
        {
            Id = ActiveOrderId,
            UserId = UserWithAnActiveOrderId,
            OrderStatusId = (int)OrderStatuses.CookingInProgress,
            DeliveryMethodId = (int)DeliveryMethods.Delivery,
            PaymentMethodId = (int)PaymentMethods.Cash,
            PizzasInOrders = new List<PizzaOrder>(),
            Pizzas = new List<Pizza>()
        };
        public static Order EditingReadyOrder = new Order()
        {
            Id = EditingReadyOrderId,
            UserId = UserWithAnEditingReadyOrderId,
            OrderStatusId = (int)OrderStatuses.InProccesOfCreating,
            DeliveryMethodId = (int)DeliveryMethods.Delivery,
            PaymentMethodId = (int)PaymentMethods.Cash,
            PizzasInOrders = new List<PizzaOrder>(),
            Pizzas = new List<Pizza>()
        };

        public static Order NewlyAddedOrder = new Order()
        {
            UserId = UserWithoutAnActiveOrderId
        };

        public static readonly Pizza ExistingPizza = new Pizza()
        {
            Id = ExistingPizzaId,
            PizzaInOrders = new List<PizzaOrder>(),
            Orders = new List<Order>()
        };

        // Is used in AddPizzaToOrderTests in AddPizzaAsync_ValidData_OrderDoesNotHaveAnyPizzaOfThisType method
        public static readonly Pizza PizzaInOrder = new Pizza()
        {
            Id = ExistingPizzaId,
            PizzaInOrders = new List<PizzaOrder>()
            {
                new PizzaOrder()
                {
                    //Pizza = PizzaInOrder,
                    PizzaId = ExistingPizzaId,
                    //Order = OrderWithPizzas,
                    OrderId = EditingReadyOrderId,
                    Amount = PizzasInOrderAmount
                }
            },
            Orders = new List<Order>() { OrderWithPizzas }
        };

        // Is used in AddPizzaToOrderTests in AddPizzaAsync_ValidData_OrderDoesNotHaveAnyPizzaOfThisType method
        public static Order OrderWithPizzas = new Order()
        {
            Id = EditingReadyOrderId,
            PizzasInOrders = new List<PizzaOrder>()
            {
                new PizzaOrder()
                {
                    //Pizza = PizzaInOrder,
                    PizzaId = ExistingPizzaId,
                    //Order = OrderWithPizzas,
                    OrderId = EditingReadyOrderId,
                    Amount = PizzasInOrderAmount
                }
            },
            Pizzas = new List<Pizza>() { PizzaInOrder }
        };

        // Is used in AddPizzaToOrderTests in AddPizzaAsync_ValidData_OrderAlreadyHasAtLeastOnePizzaOfThisType method
        public static Pizza DoubledPizzaInOrder = new Pizza()
        {
            Id = ExistingPizzaId,
            PizzaInOrders = new List<PizzaOrder>()
            {
                new PizzaOrder()
                {
                    //Pizza = PizzaInOrder,
                    PizzaId = ExistingPizzaId,
                    //Order = OrderWithPizzas,
                    OrderId = EditingReadyOrderId,
                    Amount = DoubledPizzasInOrderAmount
                }
            },
            Orders = new List<Order>() { OrderWithDoubledPizzas }
        };

        // Is used in AddPizzaToOrderTests in AddPizzaAsync_ValidData_OrderAlreadyHasAtLeastOnePizzaOfThisType method
        public static Order OrderWithDoubledPizzas = new Order()
        {
            Id = EditingReadyOrderId,
            PizzasInOrders = new List<PizzaOrder>()
            {
                new PizzaOrder()
                {
                    //Pizza = PizzaInOrder,
                    PizzaId = ExistingPizzaId,
                    //Order = OrderWithPizzas,
                    OrderId = EditingReadyOrderId,
                    Amount = DoubledPizzasInOrderAmount
                }
            },
            Pizzas = new List<Pizza>() { DoubledPizzaInOrder }
        };
    }
}
