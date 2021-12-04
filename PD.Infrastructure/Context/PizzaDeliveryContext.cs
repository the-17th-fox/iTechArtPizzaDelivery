using PD.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using PD.Domain.Constants.UsersRoles;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            List<IdentityRole<long>> usersRoles = RolesNames.GetUsersRoles();
            modelBuilder.Entity<IdentityRole<long>>().HasData(usersRoles);
            
        }
    }
}
