using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

class Worker
{
    public static void Main()
    {
        var factory = new ConnectionFactory()
        { 
            HostName = "localhost"
        };
        using(var connection = factory.CreateConnection())
        using(var channel = connection.CreateModel())
        {
            channel.QueueDeclare(
                queue: "task_queue",
                // Долговечность очереди.
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            channel.BasicQos(
                prefetchSize: 0, 
                // Одно сообщение работнику за раз.
                prefetchCount: 1,
                global: false
            );

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($" [x] Received {message}");

                int dots = message.Split(".").Length - 1;
                Thread.Sleep(dots * 1000);
                
                Console.WriteLine(" [x] Done");

                // Подтверждение успешной обработки сообщения отправляем вручную.
                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };
            channel.BasicConsume(
                queue: "task_queue",
                // Автоматической подтверждение завершения обработки сообщения отключено.
                autoAck: false,
                consumer: consumer
            );

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}