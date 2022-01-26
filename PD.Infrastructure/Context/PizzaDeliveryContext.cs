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
using PD.Domain.Constants.ForcedDBTablesData;

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

            List<IdentityRole<long>> usersRoles = RolesNames.GetRolesNames();

            modelBuilder.Entity<IdentityRole<long>>()
                .HasData(usersRoles);

            modelBuilder
                .Entity<Order>()
                .HasMany(o => o.Pizzas)
                .WithMany(p => p.Orders)
                .UsingEntity<PizzaOrder>
                (
                    j => j
                        .HasOne(pt => pt.Pizza)
                        .WithMany(t => t.PizzaInOrders)
                        .HasForeignKey(pt => pt.PizzaId),

                    j => j
                        .HasOne(pt => pt.Order)
                        .WithMany(t => t.PizzasInOrders)
                        .HasForeignKey(pt => pt.OrderId),

                    j =>
                    {
                        j.Property(pt => pt.Amount).HasDefaultValue(1);
                        j.HasKey(t => new { t.OrderId, t.PizzaId });
                        j.ToTable("PizzaOrder");
                    }
                );

            var pizzas = ForcedDBTablesData.GetPizzas();
            var ingredients = ForcedDBTablesData.GetIngredients();
            var pizzaIngredients = new List<object>
            {
                new { PizzaId = 1l, IngredientId = 1l },
                new { PizzaId = 1l, IngredientId = 3l },
                new { PizzaId = 2l, IngredientId = 2l },
                new { PizzaId = 2l, IngredientId = 3l },
                new { PizzaId = 3l, IngredientId = 4l },
                new { PizzaId = 3l, IngredientId = 3l }
            };

            modelBuilder
                .Entity<Pizza>()
                .HasData(pizzas);

            modelBuilder
                .Entity<Ingredient>()
                .HasData(ingredients);

            modelBuilder
                .Entity<Pizza>()
                .HasMany(p => p.Ingredients)
                .WithMany(i => i.Pizzas)
                .UsingEntity<IngredientPizza>
                (
                    ip => ip
                        .HasOne(pt => pt.Ingredient)
                        .WithMany(t => t.IngredientInPizzas)
                        .HasForeignKey(pt => pt.IngredientId),

                    ip => ip
                        .HasOne(pt => pt.Pizza)
                        .WithMany(t => t.IngredientsInPizza)
                        .HasForeignKey(pt => pt.PizzaId)
                ).ToTable(nameof(IngredientPizza))
                .HasKey(ip => new { ip.PizzaId, ip.IngredientId });

            modelBuilder
                .Entity<IngredientPizza>()
                .HasData(pizzaIngredients);
        }
    }
}
