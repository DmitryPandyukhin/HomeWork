using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Configuration;
using OrdersParser.Models;

namespace OrdersParser.Services
{
    internal static class DBLoader
    {
        internal static void LoadToDB(List<Order> ordersList, List<Product> productsList)
        {
            var builder = new ConfigurationBuilder();
            // установка пути к текущему каталогу
            builder.SetBasePath(Directory.GetCurrentDirectory());
            // получаем конфигурацию из файла appsettings.json
            builder.AddJsonFile("appsettings.json");
            // создаем конфигурацию
            var config = builder.Build();
            // получаем строку подключения
            string connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<MSSQLContext>();
            var Options = optionsBuilder.UseSqlServer(connectionString).Options;

            using (MSSQLContext db = new MSSQLContext(Options))
            {
                // Обновление списка товаров.
                // Контроль уникальности товаров по имени и стоимости.
                if (productsList.Count > 0)
                {
                    productsList = productsList.Distinct().ToList();

                    List<Product> checkedProductsList = new();
                    foreach (var product in productsList)
                    {
                        if (!db.Products.Any(p => p.Name == product.Name && p.Price == product.Price))
                        {
                            checkedProductsList.Add(product);
                        }
                    }

                    if (checkedProductsList.Count > 0)
                    {
                        db.Products.AddRange(checkedProductsList);
                        db.SaveChanges();
                    }
                }

                // Обновление списка пользователей.
                // Контроль уникальности пользователей
                if (ordersList.Count > 0)
                {
                    
                    List<User> usersList = ordersList.Select(o => o.User).Distinct().ToList();

                    List<User> checkedUsersList = new();

                    foreach (var user in usersList)
                    {
                        if (!db.Users.Any(u => u.FIO == user.FIO))
                            checkedUsersList.Add(user);
                    }

                    if (checkedUsersList.Count > 0)
                    {
                        db.Users.AddRange(checkedUsersList);
                        db.SaveChanges();
                    }
                }

                if (ordersList.Count > 0)
                {
                    List<Order> checkedOrderList = new();

                    // Контроль уникальности заказа.
                    foreach (var order in ordersList)
                    {
                        if (!db.Orders.Any(o => (o.No == order.No && o.Reg_date == order.Reg_date && o.Sum == order.Sum)))
                          checkedOrderList.Add(order);
                    }

                    if (checkedOrderList.Count > 0)
                    {
                        // Обновление заказов.
                        foreach (var order in checkedOrderList)
                        {
                            // Получаем существующего пользователя
                            order.UserId = db.Users.FirstOrDefault(u => u.FIO == order.User.FIO)?.UserId ?? 0;
                            order.User = null!;

                            // Обновление детализации заказа.
                            foreach (var orderDetails in order.OrderDetails)
                            {
                                orderDetails.ProductId = db.Products
                                    .FirstOrDefault(p => p.Name == orderDetails.Product.Name
                                    && p.Price == orderDetails.Product.Price)?.ProductId ?? 0;
                                orderDetails.Product = null!;
                            }
                        }

                        db.Orders.AddRange(checkedOrderList);
                        db.SaveChanges();
                    }
                }
            }
        }
    }
}
