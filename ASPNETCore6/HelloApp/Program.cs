using Microsoft.AspNetCore.Mvc.Routing;
using System.Text.Json.Serialization;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// JSON
app.Run(async (context) =>
{
    var response = context.Response;
    var request = context.Request;
    if (request.Path == "/api/user")
    {
        var responseText = "Некорректные данные";   // содержание сообщения по умолчанию

        if (request.HasJsonContentType())
        {
            // определяем параметры сериализации/десериализации
            var jsonoptions = new JsonSerializerOptions();
            // добавляем конвертер кода json в объект типа Person
            jsonoptions.Converters.Add(new PersonConverter());
            // десериализуем данные с помощью конвертера PersonConverter
            var person = await request.ReadFromJsonAsync<Person>(jsonoptions);
            if (person != null)
                responseText = $"Name: {person.Name}  Age: {person.Age}";
        }
        await response.WriteAsJsonAsync(new { text = responseText });
    }
    else
    {
        response.ContentType = "text/html; charset=utf-8";
        await response.SendFileAsync("html/index2.html");
    }
});

app.Run();
public record Person(string Name, int Age);
public class PersonConverter : JsonConverter<Person>
{
    // десериализация json
    public override Person Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var personName = "Undefined";
        var personAge = 0;
        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                var propertyName = reader.GetString();
                reader.Read();
                switch (propertyName?.ToLower())
                {
                    // если свойство age и оно содержит число
                    case "age" when reader.TokenType == JsonTokenType.Number:
                        personAge = reader.GetInt32();  // считываем число из json
                        break;
                    // если свойство age и оно содержит строку
                    case "age" when reader.TokenType == JsonTokenType.String:
                        string? stringValue = reader.GetString();
                        // пытаемся конвертировать строку в число
                        if (int.TryParse(stringValue, out int value))
                        {
                            personAge = value;
                        }
                        break;
                    case "name":    // если свойство Name/name
                        string? name = reader.GetString();
                        if (name != null)
                            personName = name;
                        break;
                }
            }
        }
        return new Person(personName, personAge);
    }
    // сериализуем объект Person в json
    public override void Write(Utf8JsonWriter writer, Person person, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("name", person.Name);
        writer.WriteNumber("age", person.Age);

        writer.WriteEndObject();
    }
}

// переадресация
/*app.Run(async (context) =>
{
    if (context.Request.Path == "/old")
    {
        context.Response.Redirect("/new");
    }
    else
    {
        if (context.Request.Path == "/new")
        {
            await context.Response.WriteAsync("New Page");
        } else
        await context.Response.WriteAsync("Main Page");
    }
});*/

// Отправка и получение формы
/*app.Run(async (context) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";

    // если обращение идет к адресу "/postuser", то получаем данные формы
    if (context.Request.Path == "/postuser")
    {
        var form = context.Request.Form;
        string name = form["name"];
        string age = form["age"];
        string[] languages = form["languages"];

        // массив в строку
        string langList = "";
        foreach (var lang in languages)
        {
            langList += $" {lang}";
        }
        // Отправляем данные с формы
        await context.Response.WriteAsync($"<div><p>Name: {name}</p>" +
            $"<p>Age: {age}</p>" +
            $"<p>Languages: {langList}</p></div>");
    }
    else
    {
        // Отправляем форму
        await context.Response.SendFileAsync("html/index.html");
    }
});*/

// отдельные параметры строки запроса
/*app.Run(async (context) =>
{
    string name = context.Request.Query["name"];
    string age = context.Request.Query["age"];
    await context.Response.WriteAsync($"{name} - {age}");
});*/
// все параметры строки запроса
/*app.Run(async (context) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";
    var stringBuilder = new System.Text.StringBuilder("<h3>Параметры строки запроса</h3><table>");
    stringBuilder.Append("<tr><td>Параметр</td><td>Значение</td></tr>");
    foreach (var param in context.Request.Query)
    {
        stringBuilder.Append($"<tr><td>{param.Key}</td><td>{param.Value}</td></tr>");
    }
    stringBuilder.Append("</table>");
    await context.Response.WriteAsync(stringBuilder.ToString());
});*/

// строка запроса отделяется от пути запроса знаком ?. Она представляет собой словарь
/*app.Run(async (context) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";
    await context.Response.WriteAsync($"<p>Path: {context.Request.Path}</p>" +
        $"<p>QueryString: {context.Request.QueryString}</p>");
});*/

// в зависимости от пути запроса возвращаем разные ответы. Так нельзя, т.к. есть
// инструменты маршрутизации
/*app.Run(async (context) =>
{
    var path = context.Request.Path;
    var now = DateTime.Now;
    var response = context.Response;

    if (path == "/date")
        await response.WriteAsync($"Date: {now.ToShortDateString()}");
    else if (path == "/time")
        await response.WriteAsync($"Time: {now.ToShortTimeString()}");
    else
        await response.WriteAsync("Hello, World!");
});
*/
// полечение строки запроса и пути запроса. Строка запроса содержит параметры.
/*app.Run(async (context) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";
    await context.Response.WriteAsync($"<p>Path: {context.Request.Path}</p>" +
        $"<p>QueryString: {context.Request.QueryString}</p>");
});*/

// получение пути запроса
//app.Run(async (context) => await context.Response.WriteAsync($"Path: {context.Request.Path}"));

// получение всех заголовков запроса и вывод их на страницу
/*app.Run(async (context) =>
{
    var acceptHeaderValue = context.Request.Headers.Accept;
    await context.Response.WriteAsync($"Accept: {acceptHeaderValue}");
});*/
/*app.Run(async (context) =>
{
    context.Response.ContentType = "text/html; charset=utf-8"; // отвечать на запрос будем с помощью html
    var stringBuilder = new System.Text.StringBuilder("<table>");

    // Заполняем таблицу
    foreach(var header in context.Request.Headers)
    {
        stringBuilder.Append($"<tr><td>{header.Key}</td><td>{header.Value}</td></tr>");
    }
    stringBuilder.Append("</table>");

    await context.Response.WriteAsync(stringBuilder.ToString());
});*/


// отправка Html кода
/*app.Run(async (content) =>
{
    var response = content.Response;
    response.ContentType = "text/html; charset=utf-8";
    await response.WriteAsync("<h2>Hello, World!</h2><h3>Welcome to ASP.NET Core</h3>");
});*/

// установка кодов статуч <summary>
/*app.Run(async (content) =>
{
    var response = content.Response;
    response.StatusCode = 404;
    await response.WriteAsync("Resource Not Found");
});^*/

// Использование HttpResponse
/*app.Run(async (content) =>
{
    var response = content.Response;
    // Установка заголовков
    response.Headers.ContentLanguage = "ru-Ru";
    // заголовок ContentType
    // или так: response.ContentType = "text/plain; charset=utf-8";
    response.Headers.ContentType = "text/plain; charset=utf-8";
    // добавление других заголовков. Например, свой заголовок secret-id
    response.Headers.Append("secret-id", "256");
    await response.WriteAsync("Привет, мир!");
});*/

///  <summary>
/// самый простой способ добавить компонент - это вызвать метод расширения Run. Этот метод создает 
/// терминальный компонент, который завершает обработку запроса.
/// Не надо путать с методом Run(), который запускает приложение.
/// </summary>
// app.Run(async (context) => await context.Response.WriteAsync("Hello, World!"));

/// подключение в конвейер middleware компонента
//app.UseWelcomePage();

/// пример создания маршрута. Здесь создеатся корневой маршрут.
//app.MapGet("/", () => "Hello World!");

/// Запуск приложения
