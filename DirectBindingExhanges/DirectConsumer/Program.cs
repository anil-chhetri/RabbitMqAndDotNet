using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace DirectConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("Hello World!");

            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "anil",
                Password = "anil",
                VirtualHost = "HeaderExchange"
            };

            var conn = factory.CreateConnection();

            var channel = conn.CreateModel();

            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume("direct.queue", false, consumer);
            consumer.Received += (model, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                System.Console.WriteLine($"message: {message}");

                channel.BasicAck(ea.DeliveryTag, false);
            };

            System.Console.WriteLine("at the end.");
            Console.ReadKey();

        }
    }
}
