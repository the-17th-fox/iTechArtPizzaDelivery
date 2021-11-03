using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iTechArtPizzaDelivery.Domain.Entities
{
    public class Ingredient
    {
        [Key]
        public int IngredientID { get; set; }

        [BindRequired]
        public string Name { get; set; }

        [BindRequired]
        public float PricePerUnit { get; set; }
    }
}
