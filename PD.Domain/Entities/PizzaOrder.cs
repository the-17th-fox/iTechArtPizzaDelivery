using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Domain.Entities
{
    public class PizzaOrder
    {
        public long PizzaId { get; set; }
        public long OrderId { get; set; }
        public int Amount { get; set; }

        public Order Order { get; set; }
        public Pizza Pizza { get; set; }
    
        
    }
}
