//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using iTechArtPizzaDelivery.Domain.Entities;
//using iTechArtPizzaDelivery.Domain.Interfaces;

//namespace iTechArtPizzaDelivery.Infrastructure.Repository.Fake
//{
//    public class PizzasFakeRepository : IPizzasRepository
//    {
//        // Default pizzas list
//        private static List<Pizza> _pizzas = new List<Pizza> 
//        {
//            new Pizza
//                (
//                    id: 0,
//                    name: "Banana Pizza",
//                    description: "This is a Banana pizza"
//                ),

//                    new Pizza
//                (
//                    id: 1,
//                    name: "Tomato Pizza",
//                    description: "This is a Tomato pizza"
//                ),

//                    new Pizza
//                (
//                    id: 2,
//                    name: "Cheese Pizza",
//                    description: "This is a Cheese pizza"
//                ),

//                    new Pizza
//                (
//                    id: 3,
//                    name: "Apple Pizza",
//                    description: "This is an Apple pizza"
//                ),
//        };

//        public List<Pizza> GetAllPizzas() => _pizzas;

//        public Pizza FindPizzaById(ulong id)
//        {
//            Pizza lookingPizza = (from pizza in _pizzas
//                                 where (pizza.PizzaID == id)
//                                 select pizza).FirstOrDefault();

//            return lookingPizza;
//        }

//        public void PostPizza(string name, string description)
//        {
//            _pizzas.Add
//            (
//                new Pizza
//                (
//                    id: (ulong)_pizzas.Count, // List length is equal to the next pizzas's ID
//                    name: name,
//                    description: description
//                )
//            );
//        }
//    }
//}
