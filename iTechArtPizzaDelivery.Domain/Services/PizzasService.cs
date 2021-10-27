using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTechArtPizzaDelivery.Domain.Interfaces;

namespace iTechArtPizzaDelivery.Domain.Services
{
    public class PizzasService
    {
        private readonly IPizzasRepository _pizzasRepository;
        public PizzasService(IPizzasRepository context)
        {
            _pizzasRepository = context;
        }

        // TODO: Add GetAll and PizasServ Interface 
    }
}
