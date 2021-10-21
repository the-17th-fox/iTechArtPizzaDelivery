using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iTechArtPizza.Web.Entities
{
    // ?: Each ID increases by 1 after every page reload
    public class Pizza
    { 
        public ulong Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public Pizza(ulong id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
        }
    }
}
