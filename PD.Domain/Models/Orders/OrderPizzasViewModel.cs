using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PD.Domain.Models
{
    public class OrderPizzasViewModel
    {
        public long Id { get; set; }
        public List<PizzaInOrderViewModel> Pizzas { get; set; }
    }
}
