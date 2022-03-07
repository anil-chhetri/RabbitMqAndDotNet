using System;
using System.Text;
using RabbitMQ.Client;

namespace DirectPublisher
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

            channel.QueueDeclare("direct.queue", true, false, false, null);

            //skipping exchageDeclare cause direct binding doesn't required exchanges.
            // In this case routing key should be same as queue name to which message is send.

            channel.BasicPublish(string.Empty, "direct.queue", false, null, Encoding.UTF8.GetBytes("message without exchange."));

            System.Console.WriteLine("message published.");
            Console.ReadLine();

        }
    }
}
