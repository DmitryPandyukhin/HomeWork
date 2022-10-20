using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace SportsStore
{
    public class Startup
    {
        public IConfiguration Configuration;
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;
        public void ConfigureServices(IServiceCollection services)
        {
            // Настройка EF Core
            // AddDbContext настраивает службы для контекста БД
            // ApplicationDbContext одноименного класса.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration["Data:SportsStoreProducts:" +
                    "ConnectionString"]));
            // Регистрируем службу хранилища. Позволит контроллеру
            // получать реализующие интерфейс IProductRepository
            // объекты, не зная, какой класс используется.
            // Таким образом реализуется слабосвязанность компонентов.
            services.AddTransient<IProductRepository,
                EFProductRepository>();
            // Настройка разделяемых объектов
            services.AddMvc();
        }
        // Настройка средств, получающих и обрабатывающих Http=запросы.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Отображает детали исключения. Только для разработки. Убрать при развертывании.
            app.UseDeveloperExceptionPage();
            // Добавляет сообщение в Http-ответы
            app.UseStatusCodePages();
            // Поддержка обслуживания содержимого папки wwwroot
            app.UseStaticFiles();
            // Включает инфраструктуру ASP.NET Core MVC
            app.UseMvc(routes => 
            {
                routes.MapRoute(
                    name: "paginaation",
                    template: "Products/Page{productPage}",
                    defaults: 
                        new { Controller = "Product", action = "List" });
                // Настраиваем маршрут. Этот метод действия будет вызываться
                // по умолчанию (если в URL не указазано иное)
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Product}/{action=List}/{id?}");
            });
            SeedData.EnsurePopulated(app);
        }
    }
}
