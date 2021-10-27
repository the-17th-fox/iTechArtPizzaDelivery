using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iTechArtPizzaDelivery.Domain.Entities
{
    public class Ingredient
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public float PricePerUnit { get; set; }

        public Ingredient(ulong id, string name, float pricePerUnit)
        {
            Id = id;
            Name = name;
            PricePerUnit = pricePerUnit;
        }
    }
}
