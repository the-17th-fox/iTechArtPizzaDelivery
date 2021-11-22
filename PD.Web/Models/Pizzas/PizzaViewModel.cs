using AutoMapper;
using PD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PD.Web.Models
{ 
    /// <summary>
    /// GetById
    /// </summary>
    public class PizzaViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ShortIngredientViewModel> Ingredients { get; set; } = new List<ShortIngredientViewModel>();
    }
}
