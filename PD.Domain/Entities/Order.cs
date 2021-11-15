using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Domain.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public virtual List<Pizza> Pizzas { get; set; }
        public string Adress { get; set; }
        public bool IsPaid { get; set; } = false;

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public int? PromoCodeId { get; set; }
        [ForeignKey("PromoCodeId")]
        public PromoCode PromoCode { get; set; }
    }
}
