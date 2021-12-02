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
using PD.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
                typeof(OrdersProfile),
                typeof(IngredientsProfile),
                typeof(UsersProfile)
            );

            //DBCONTEXT SETTINGS
            services.AddDbContext<PizzaDeliveryContext>
                (context => context.UseSqlServer(Configuration["connectionStrings:DatabaseConnection"]));

            //IDENTITY SERVICES AND OPTIONS
            services.AddIdentity<User, IdentityRole<long>>(options => {
                options.Password.RequiredLength = 5; 
                options.Password.RequireNonAlphanumeric = false; 
                options.Password.RequireLowercase = false; 
                options.Password.RequireUppercase = false; 
                options.Password.RequireDigit = false;
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<PizzaDeliveryContext>();

            //AUTHENTICATION 
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                 .AddJwtBearer(options =>
                 {
                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuer = false,
                         ValidateAudience = false,
                         ValidateLifetime = false,
                         ValidateIssuerSigningKey = false
                     };
                 });

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

            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
