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
        var responseText = "������������ ������";   // ���������� ��������� �� ���������

        if (request.HasJsonContentType())
        {
            // ���������� ��������� ������������/��������������
            var jsonoptions = new JsonSerializerOptions();
            // ��������� ��������� ���� json � ������ ���� Person
            jsonoptions.Converters.Add(new PersonConverter());
            // ������������� ������ � ������� ���������� PersonConverter
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
    // �������������� json
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
                    // ���� �������� age � ��� �������� �����
                    case "age" when reader.TokenType == JsonTokenType.Number:
                        personAge = reader.GetInt32();  // ��������� ����� �� json
                        break;
                    // ���� �������� age � ��� �������� ������
                    case "age" when reader.TokenType == JsonTokenType.String:
                        string? stringValue = reader.GetString();
                        // �������� �������������� ������ � �����
                        if (int.TryParse(stringValue, out int value))
                        {
                            personAge = value;
                        }
                        break;
                    case "name":    // ���� �������� Name/name
                        string? name = reader.GetString();
                        if (name != null)
                            personName = name;
                        break;
                }
            }
        }
        return new Person(personName, personAge);
    }
    // ����������� ������ Person � json
    public override void Write(Utf8JsonWriter writer, Person person, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("name", person.Name);
        writer.WriteNumber("age", person.Age);

        writer.WriteEndObject();
    }
}

// �������������
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

// �������� � ��������� �����
/*app.Run(async (context) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";

    // ���� ��������� ���� � ������ "/postuser", �� �������� ������ �����
    if (context.Request.Path == "/postuser")
    {
        var form = context.Request.Form;
        string name = form["name"];
        string age = form["age"];
        string[] languages = form["languages"];

        // ������ � ������
        string langList = "";
        foreach (var lang in languages)
        {
            langList += $" {lang}";
        }
        // ���������� ������ � �����
        await context.Response.WriteAsync($"<div><p>Name: {name}</p>" +
            $"<p>Age: {age}</p>" +
            $"<p>Languages: {langList}</p></div>");
    }
    else
    {
        // ���������� �����
        await context.Response.SendFileAsync("html/index.html");
    }
});*/

// ��������� ��������� ������ �������
/*app.Run(async (context) =>
{
    string name = context.Request.Query["name"];
    string age = context.Request.Query["age"];
    await context.Response.WriteAsync($"{name} - {age}");
});*/
// ��� ��������� ������ �������
/*app.Run(async (context) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";
    var stringBuilder = new System.Text.StringBuilder("<h3>��������� ������ �������</h3><table>");
    stringBuilder.Append("<tr><td>��������</td><td>��������</td></tr>");
    foreach (var param in context.Request.Query)
    {
        stringBuilder.Append($"<tr><td>{param.Key}</td><td>{param.Value}</td></tr>");
    }
    stringBuilder.Append("</table>");
    await context.Response.WriteAsync(stringBuilder.ToString());
});*/

// ������ ������� ���������� �� ���� ������� ������ ?. ��� ������������ ����� �������
/*app.Run(async (context) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";
    await context.Response.WriteAsync($"<p>Path: {context.Request.Path}</p>" +
        $"<p>QueryString: {context.Request.QueryString}</p>");
});*/

// � ����������� �� ���� ������� ���������� ������ ������. ��� ������, �.�. ����
// ����������� �������������
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
// ��������� ������ ������� � ���� �������. ������ ������� �������� ���������.
/*app.Run(async (context) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";
    await context.Response.WriteAsync($"<p>Path: {context.Request.Path}</p>" +
        $"<p>QueryString: {context.Request.QueryString}</p>");
});*/

// ��������� ���� �������
//app.Run(async (context) => await context.Response.WriteAsync($"Path: {context.Request.Path}"));

// ��������� ���� ���������� ������� � ����� �� �� ��������
/*app.Run(async (context) =>
{
    var acceptHeaderValue = context.Request.Headers.Accept;
    await context.Response.WriteAsync($"Accept: {acceptHeaderValue}");
});*/
/*app.Run(async (context) =>
{
    context.Response.ContentType = "text/html; charset=utf-8"; // �������� �� ������ ����� � ������� html
    var stringBuilder = new System.Text.StringBuilder("<table>");

    // ��������� �������
    foreach(var header in context.Request.Headers)
    {
        stringBuilder.Append($"<tr><td>{header.Key}</td><td>{header.Value}</td></tr>");
    }
    stringBuilder.Append("</table>");

    await context.Response.WriteAsync(stringBuilder.ToString());
});*/


// �������� Html ����
/*app.Run(async (content) =>
{
    var response = content.Response;
    response.ContentType = "text/html; charset=utf-8";
    await response.WriteAsync("<h2>Hello, World!</h2><h3>Welcome to ASP.NET Core</h3>");
});*/

// ��������� ����� ������ <summary>
/*app.Run(async (content) =>
{
    var response = content.Response;
    response.StatusCode = 404;
    await response.WriteAsync("Resource Not Found");
});^*/

// ������������� HttpResponse
/*app.Run(async (content) =>
{
    var response = content.Response;
    // ��������� ����������
    response.Headers.ContentLanguage = "ru-Ru";
    // ��������� ContentType
    // ��� ���: response.ContentType = "text/plain; charset=utf-8";
    response.Headers.ContentType = "text/plain; charset=utf-8";
    // ���������� ������ ����������. ��������, ���� ��������� secret-id
    response.Headers.Append("secret-id", "256");
    await response.WriteAsync("������, ���!");
});*/

///  <summary>
/// ����� ������� ������ �������� ��������� - ��� ������� ����� ���������� Run. ���� ����� ������� 
/// ������������ ���������, ������� ��������� ��������� �������.
/// �� ���� ������ � ������� Run(), ������� ��������� ����������.
/// </summary>
// app.Run(async (context) => await context.Response.WriteAsync("Hello, World!"));

/// ����������� � �������� middleware ����������
//app.UseWelcomePage();

/// ������ �������� ��������. ����� ��������� �������� �������.
//app.MapGet("/", () => "Hello World!");

/// ������ ����������
