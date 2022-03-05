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
            Console.WriteLine("Consumer started....");

            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
                VirtualHost = "DirectExchange",
                UserName = "anil",
                Password = "anil"
            };

            var conn = factory.CreateConnection();
            var channel = conn.CreateModel();

            var consumer = new EventingBasicConsumer(channel);

            channel.BasicConsume("info", false, consumer);

            consumer.Received += (model, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                System.Console.WriteLine($"message: {message}");
                channel.BasicAck(ea.DeliveryTag, false);

            };

            System.Console.WriteLine("Consume is completed. Press any key to exit.");
            Console.ReadLine();
        }
    }
}
