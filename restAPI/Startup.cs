using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using restAPI.Entities;
using restAPI.Services;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using restAPI.Models.Validators;
using restAPI.Models;
using FluentValidation.AspNetCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace restAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var authenticationService = new AuthenticationSettings();
            Configuration.GetSection("Authentication").Bind(authenticationService);
            services.AddSingleton(authenticationService);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = authenticationService.JwtIssuer,
                    ValidAudience = authenticationService.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationService.JwtKey))
                    
                };
        });
            /*services.AddScoped<IWeatherForecastService>();*/
            services.AddControllers()
                .AddFluentValidation();
            services.AddAuthorization(option =>
            {
                option.AddPolicy("HasNationality", builder => builder.RequireClaim("Nationality","polish","")); //jedyna s³uszna narodowoœæ
                option.AddPolicy("AtLeast20", builder => builder.AddRequirements(new MiniumumAgeRequireMent(20))); //jedyna s³uszna narodowoœæ
            });
            services.AddDbContext<RestaurantDbContext>(); // dla serwisu bazy danych
            services.AddScoped<RestaurantSeeder>();
            services.AddAutoMapper(this.GetType().Assembly);
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IRestaurantService, RestaurantService>();
            services.AddScoped<IDishesService, DishesService>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
  
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, RestaurantSeeder seeder)
        {
            seeder.Seed();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

