using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PD.Domain.Entities
{
    public class Ingredient
    {
        [Key]
        public int IngredientID { get; set; }

        public string Name { get; set; }
    }
}
