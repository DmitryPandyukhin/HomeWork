﻿using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace SportsStore.Models
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices
                .GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product
                    {
                        Name = "Байдарка",
                        Description = "Лодка для одного человека",
                        Category = "Водные виды спорта",
                        Price = 275
                    },
                    new Product
                    {
                        Name = "Спасательный жилет",
                        Description = "Защитный и модный",
                        Category = "Водные виды спорта",
                        Price = 48.95m
                    },
                    new Product
                    {
                        Name = "Футбольный мяч",
                        Description = "Размер и вес, одобренные ФИФА",
                        Category = "Футбол",
                        Price = 19.50m
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
