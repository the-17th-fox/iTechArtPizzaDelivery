using AutoMapper;
using PD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PD.Domain.Models
{ 
    public class PizzaViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public List<ShortIngredientViewModel> Ingredients { get; set; }
    }
}
