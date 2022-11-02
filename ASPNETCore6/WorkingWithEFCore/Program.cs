using static System.Console;
using Packt.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.Run(async (context) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";
    await context.Response.WriteAsync(QueryingCategories().ToString());
    
});

//app.MapGet("/", () => "Hello World!");

app.Run();

static StringBuilder QueryingCategories()
{
    StringBuilder stringBuilder = new("<h3>Категории и количество товаров:</h3><table>");

    using (var db = new Northwind())
    {
        stringBuilder.Append("<tr><td>Категория</td><td>Количество товара</td></tr>");

        IQueryable<Category> cats = db.Categories
            .Include(c => c.Products);

        foreach (Category c in cats)
        {
            stringBuilder.Append($"<tr><td>{c.CategoryName}</td><td>{c.Products.Count}</td></tr>");
        }

        stringBuilder.Append("</table>");
    }

    return stringBuilder;
}
