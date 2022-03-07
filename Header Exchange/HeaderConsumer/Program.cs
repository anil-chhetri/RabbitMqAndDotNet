using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace HeaderConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
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
            channel.BasicConsume("my.queue2", false, consumer);
            consumer.Received += (model, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                System.Console.WriteLine($"Message: {message}");

                channel.BasicAck(ea.DeliveryTag, false);
            };

            System.Console.WriteLine("quite");
            Console.ReadLine();
        }
    }
}
