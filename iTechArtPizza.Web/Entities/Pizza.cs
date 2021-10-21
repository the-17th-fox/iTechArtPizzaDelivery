﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iTechArtPizza.Web.Entities
{
    public class Pizza
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //public List<Ingredient> Ingredients { get; set; }
        //public /*Image*/ Image { get; set; }

        public Pizza(ulong id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}
