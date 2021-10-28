using iTechArtPizzaDelivery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTechArtPizzaDelivery.Domain.Services
{
    public interface IPizzasService
    {
        public List<Pizza> GetAll();
        public Pizza FindById(ulong id);
        public void Post(string name, string description);
    }
}
