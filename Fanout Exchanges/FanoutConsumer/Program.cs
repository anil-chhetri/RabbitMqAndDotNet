using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace FanoutConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectionFactory facotry = new()
            {
                UserName = "anil",
                Password = "anil",
                VirtualHost = "FanOutExchanges",
                Port = 5672,
                HostName = "localhost"
            };

            var conn = facotry.CreateConnection();
            var channel = conn.CreateModel();

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                var message = Encoding.ASCII.GetString(ea.Body.ToArray());
                System.Console.WriteLine(message);
            };

            var tag = channel.BasicConsume("my.queue1", true, consumer);
            Console.WriteLine("WAiting for messages. press any key to exist.");
            Console.ReadKey();

        }
    }
}
