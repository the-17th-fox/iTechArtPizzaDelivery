using PD.Domain.Interfaces;
using PD.Domain.Services;
using PD.Infrastructure.Context;
using PD.Infrastructure.Repositories.EFRepositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using PD.Web.Models.Profiles;

namespace PD.Domain
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        { 
            //DOMAIN
            services.AddScoped<IPizzasService, PizzasService>();
            services.AddScoped<IIngredientsService, IngredientsService>();
            services.AddScoped<IOrdersService, OrdersService>();
            services.AddScoped<IPromoCodesService, PromoCodesService>();
            services.AddScoped<IUsersService, UsersService>();

            //INFRASTRUCTURE
            services.AddScoped<IPizzasRepository, PizzasEFRepository>();
            services.AddScoped<IIngredientsRepository, IngredientsEFRepository>();
            services.AddScoped<IOrdersRepository, OrdersEFRepository>();
            services.AddScoped<IPromoCodesRepository, PromoCodesEFRepository>();
            services.AddScoped<IUsersRepository, UsersEFRepository>();

            //AUTOMAPPER PROFILES
            services.AddAutoMapper(
                typeof(PizzasProfile), 
                typeof(IngredientsProfile), 
                typeof(OrdersProfile)
                );

            //DBCONTEXT SETTINGS
            services.AddDbContext<PizzaDeliveryContext>
                (x => x.UseSqlServer(Configuration["connectionStrings:DatabaseConnection"]));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "iTechArtPizzaDelivery", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "iTechArtPizza.Web v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
