using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iTechArtPizzaDelivery.Domain.Entities
{
    public class Ingredient
    {
        public Ingredient(int ingredientID, string name, float pricePerUnit)
        {
            IngredientID = ingredientID;
            Name = name;
            PricePerUnit = pricePerUnit;
        }

        public int IngredientID { get; set; }
        public string Name { get; set; }
        public float PricePerUnit { get; set; }

        
    }
}
