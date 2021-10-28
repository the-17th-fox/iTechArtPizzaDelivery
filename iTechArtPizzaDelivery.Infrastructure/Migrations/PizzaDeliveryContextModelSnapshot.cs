﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using iTechArtPizzaDelivery.Infrastructure.Context;

namespace iTechArtPizzaDelivery.Infrastructure.Migrations
{
    [DbContext(typeof(PizzaDeliveryContext))]
    partial class PizzaDeliveryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("iTechArtPizzaDelivery.Domain.Entities.Ingredient", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(20,0)")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("PizzaId")
                        .HasColumnType("decimal(20,0)");

                    b.Property<float>("PricePerUnit")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("PizzaId");

                    b.ToTable("Ingredient");
                });

            modelBuilder.Entity("iTechArtPizzaDelivery.Domain.Entities.Pizza", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(20,0)")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Pizzas");
                });

            modelBuilder.Entity("iTechArtPizzaDelivery.Domain.Entities.Ingredient", b =>
                {
                    b.HasOne("iTechArtPizzaDelivery.Domain.Entities.Pizza", null)
                        .WithMany("Ingredients")
                        .HasForeignKey("PizzaId");
                });

            modelBuilder.Entity("iTechArtPizzaDelivery.Domain.Entities.Pizza", b =>
                {
                    b.Navigation("Ingredients");
                });
#pragma warning restore 612, 618
        }
    }
}