using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iTechArtPizza.Web.Entities;

namespace iTechArtPizza.Web.Interfaces
{
    interface IPizzasRepository
    {
        public List<Pizza> GetPizzasInfo();
        public List<Pizza> GetPizzasTitles();
        public void PostPizza(string title, string description);
    }
}
