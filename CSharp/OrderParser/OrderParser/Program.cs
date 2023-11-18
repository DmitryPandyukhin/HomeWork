using OrdersParser.Models;
using OrdersParser.Services;

string? path;
bool? response = null;

// Ввод данных.
do
{
    Console.Write("Введите путь к загружаемому файлу: ");
    path = Console.ReadLine();

    if (File.Exists(path))
    {
        response = true;
    }
    else
    {
        Console.Write("Файл не найден. Продолжить? (y/n)");
        if (Console.ReadLine()?.Trim().ToLower() == "n")
            response = false;
    }

} while (response == null);

// Парсинг и загрузка в БД.
try
{
    if (response == true)
    {
        List<Order> ordersList = new();
        List<Product> productsList = new();
        XMLParser.LoadOrders(path, ref ordersList, ref productsList);
        DBLoader.LoadToDB(ordersList, productsList);
        Console.WriteLine("Загрузка успешно завершена!");
    }
    {
        Console.WriteLine("Загрузка прервана!");
    }
}
catch (Exception ex)
{
    Console.WriteLine("Загрузка завершена с ошибками!");
    Console.WriteLine(ex.Message);
    Console.WriteLine(ex.InnerException);
}

Console.WriteLine("Нажмите любую клавишу для выхода...");
Console.ReadKey();