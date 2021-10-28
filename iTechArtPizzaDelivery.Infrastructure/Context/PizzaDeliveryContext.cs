using iTechArtPizzaDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTechArtPizzaDelivery.Infrastructure.Context
{
    public class PizzaDeliveryContext : DbContext
    {
        private const string _connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=myDataBase;Trusted_Connection = True;";
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<PromoCode> PromoCodes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

    }
}
