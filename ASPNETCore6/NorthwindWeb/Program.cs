using System.IO;
using Microsoft.EntityFrameworkCore;
using Packt.Shared;

var builder = WebApplication.CreateBuilder(args);

string databasePath = Path.Combine("..", "Northwind.db");
builder.Services.AddDbContext<Northwind>(options =>
  options.UseSqlite($"Data Source={databasePath}"));

builder.Services.AddRazorPages();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// перенаправление на https
app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapRazorPages();

app.MapGet("/hello", async (context) =>
await context.Response.WriteAsync("Hello World!"));

app.Run();