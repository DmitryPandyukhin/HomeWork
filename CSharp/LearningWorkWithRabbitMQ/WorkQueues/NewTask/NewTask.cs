using System;
using RabbitMQ.Client;
using System.Text;

class NewTask
{
    public static void Main(string[] args)
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost"
        };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(
                queue: "task_queue",
                // Долговечность очереди.
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            var message = GetMessage(args);
            var body = Encoding.UTF8.GetBytes(message);

            var properties = channel.CreateBasicProperties();
            // Сообщения в очереди как постоянные
            properties.Persistent = true;

            channel.BasicPublish(exchange: "",
                                 routingKey: "task_queue",
                                 basicProperties: properties,
                                 body: body);
            Console.WriteLine($" [x] Send {message}");
        }

        
    }

    private static string GetMessage(string[] args)
    {
        return ((args.Length > 0) ? string.Join(" ", args) : "Hello, World!");
    }
}