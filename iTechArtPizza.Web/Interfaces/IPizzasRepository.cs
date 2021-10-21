using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iTechArtPizza.Web.Entities;

namespace iTechArtPizza.Web.Interfaces
{
    interface IPizzasRepository
    {
        public List<Pizza> GetAll();
    }
}
