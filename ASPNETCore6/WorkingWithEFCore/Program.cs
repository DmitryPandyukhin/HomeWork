using static System.Console;
using Packt.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Net.NetworkInformation;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (context) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";
    if (context.Request.Path == "/postuser")
    {
        var form = context.Request.Form;
        string inputnumber = form["inputnumber"];
        string inputsting = form["inputsting"];

        // Отправляем данные с формы
        
        
        /*if (AddProduct(6, "Bob's Burgers", 500M))
            await context.Response.WriteAsync(QueringProducts(inputnumber).ToString());
        else 
            await context.Response.WriteAsync("<h>Неудача</h>");*/
        //await context.Response.WriteAsync(QueringProducts(inputnumber).ToString());
        //await context.Response.WriteAsync(FilteredIncludes(inputnumber).ToString());
        //await context.Response.WriteAsync(QueryingCategories().ToString()); // данные не вводить

    }
    else
    {
        // Отправляем форму
        await context.Response.SendFileAsync("html/index.html");
    }
});

//app.MapGet("/", () => "Hello World!");

app.Run();

// добавление записи в БД
static bool AddProduct(
    int categoryID, string productName, decimal? price)
{
    using(var db = new Northwind())
    {
        var newProduct = new Product()
        {
            CategoryID = categoryID,
            ProductName = productName,
            Cost = price
        };

        db.Products.Add(newProduct);

        int affected = db.SaveChanges();
        // количество измененных записей
        return affected == 1;
    }
}

// логи и теги
static StringBuilder QueringProducts(string price)
{

    StringBuilder stringBuilder = new("<h3>Вывод товаров не ниже определенной стоимости</br>" +
        "с сортировкой:</h3><table>");
    using (var db = new Northwind())
    {
        var loggerFactory = db.GetService<ILoggerFactory>();
        loggerFactory.AddProvider(new ConsoleLoggerProvider());

        decimal cost = decimal.Parse(price);

        IQueryable<Product> prods = db.Products
            // Теги позволят добавить комментарии в лог
            .TagWith("Фильтрация стоимости товара и сортировка")
            .Where(p => p.Cost >= cost)
            .OrderByDescending(p => p.Cost);

        stringBuilder.Append("<tr><td>Идентификатор</td><td>Название товара</td>" +
            "<td>Стоимость</td><td>Количество товара на складе</td></tr>");

        foreach (Product p in prods)
        {
            stringBuilder.Append($"<tr><td>{p.ProductID}</td><td>{p.ProductName}</td>" +
                $"<td>{p.Cost}</td><td>{p.Stock}</td></tr>");
        }

        stringBuilder.Append("</table>");
    }

    return stringBuilder;
}

static StringBuilder FilteredIncludes(string minUnit)
{
    StringBuilder stringBuilder = new("<h3>Фильтрация по количеству товара на складе:</h3><table>");
    using (var db = new Northwind())
    {
        int stock = int.Parse(minUnit);

        IQueryable<Category> cats = db.Categories
            .Include(c => c.Products.Where(p => p.Stock >= stock));

        foreach (Category c in cats)
        {
            stringBuilder.Append("<tr><td>Категория</td><td>Количество видов товара</td></tr>");
            stringBuilder.Append($"<tr><td>{c.CategoryName}</td><td>{c.Products.Count}</td></tr>");

            stringBuilder.Append($"<tr><td>Название товара</td><td>Единиц товара на складе</td></tr>");
            foreach (Product p in c.Products)
            {
                stringBuilder.Append($"<tr><td>{p.ProductName}</td><td>{p.Stock}</td></tr>");
            }
            stringBuilder.Append($"<tr><td>---</td><td>---</td></tr>");
        }

        stringBuilder.Append("</table>");

        // Отображение сгенерированного запроса к БД
        stringBuilder.Append($"<p>Запрос выглядит так: {cats.ToQueryString()}</p>");
    }


    return stringBuilder;
}

//Логирование и ленивая загрузка
static StringBuilder QueryingCategories()
{

    StringBuilder stringBuilder = new("<h3>Категории и количество товаров:</h3><table>");

    using (var db = new Northwind())
    {
        var logFactory = db.GetService<ILoggerFactory>();
        logFactory.AddProvider(new ConsoleLoggerProvider());

        stringBuilder.Append("<tr><td>Категория</td><td>Количество товара</td></tr>");

        // явная загрузка
        IQueryable<Category> cats = db.Categories;
            // отключение ленивой загрузки
        db.ChangeTracker.LazyLoadingEnabled = false;
        foreach (Category c in cats)
        {
            var products = db.Entry(c).Collection(c2 => c2.Products);
            if (!products.IsLoaded)
                products.Load();
            stringBuilder.Append($"<tr><td>{c.CategoryName}</td><td>{c.Products.Count}</td></tr>");
        }
        

        // ленивая загрузка
        //IQueryable<Category> cats = db.Categories;

        // жадная загрузка
        /*IQueryable<Category> cats = db.Categories
            .Include(c => c.Products); */

        /*foreach (Category c in cats)
        {
            stringBuilder.Append($"<tr><td>{c.CategoryName}</td><td>{c.Products.Count}</td></tr>");
        }*/

        stringBuilder.Append("</table>");
    }

    return stringBuilder;
}
