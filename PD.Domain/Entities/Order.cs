using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Domain.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? PromoCodeId { get; set; }
        public List<Pizza> Pizzas { get; set; }
        public string Adress { get; set; }
        public bool IsPaid { get; set; } = false;
    }
}
