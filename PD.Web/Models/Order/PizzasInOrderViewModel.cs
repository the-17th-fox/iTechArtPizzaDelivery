﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PD.Web.Models
{
    public class PizzasInOrderViewModel
    {
        public long Id { get; set; }
        public List<ShortPizzaViewModel> Pizzas { get; set; }
    }
}