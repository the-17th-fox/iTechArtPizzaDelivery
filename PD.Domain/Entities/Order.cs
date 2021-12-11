using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PD.Domain.Constants.OrderStatuses;

namespace PD.Domain.Entities
{
    public class Order
    {
        public long Id { get; set; }
        public List<Pizza> Pizzas { get; set; }
        public string Adress { get; set; }
        public bool IsPaid { get; set; } = false;
        public string DeliveryStatus { get; set; }
        public string DeliveryMethod { get; set; }
        public string Description { get; set; }

        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public long? PromoCodeId { get; set; }
        [ForeignKey("PromoCodeId")]
        public PromoCode PromoCode { get; set; }
    }
}
