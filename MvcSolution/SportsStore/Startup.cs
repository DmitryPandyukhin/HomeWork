using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace SportsStore
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
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
            app.UseMvc(routes => { });
        }
    }
}
