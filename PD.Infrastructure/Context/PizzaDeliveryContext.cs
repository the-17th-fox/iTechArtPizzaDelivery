using PD.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace PD.Infrastructure.Context
{
    public class PizzaDeliveryContext : IdentityDbContext<User, IdentityRole<long>, long>
    {
        public PizzaDeliveryContext(DbContextOptions<PizzaDeliveryContext> options) : base(options)
        { }

        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<PromoCode> PromoCodes { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
