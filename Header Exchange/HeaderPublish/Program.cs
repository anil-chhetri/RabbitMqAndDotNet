using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;

namespace HeaderPublish
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
                VirtualHost = "HeaderExchange",
                UserName = "anil",
                Password = "anil"
            };

            var conn = factory.CreateConnection();
            var channel = conn.CreateModel();

            channel.QueueDeclare("my.queue1", true, false, false, null);
            channel.QueueDeclare("my.queue2", true, false, false, null);


            channel.ExchangeDeclare("headerExchange", "headers", true, false, null);


            var properties = channel.CreateBasicProperties();
            properties.Headers = new Dictionary<string, object>();
            properties.Headers.Add("content", "image");
            properties.Headers.Add("format", "jpg");

            channel.QueueBind("my.queue1", "headerExchange", "", new Dictionary<string, object>() { { "x-match", "any" }, { "content", "image" }, { "format", "jpg" } });
            channel.QueueBind("my.queue2", "headerExchange", "", new Dictionary<string, object>() { { "x-match", "all" }, { "content", "image" }, { "format", "jpg" } });

            channel.BasicPublish("headerExchange", "", false, properties, Encoding.UTF8.GetBytes("hello this is message. with jpg format"));

            var properties2 = channel.CreateBasicProperties();
            properties2.Headers = new Dictionary<string, object>();
            properties2.Headers.Add("content", "image");
            properties2.Headers.Add("format", "bmp");

            channel.BasicPublish("headerExchange", "", false, properties2, Encoding.UTF8.GetBytes("hello this is message. with bmp format"));


            Console.WriteLine("message published");
            Console.ReadLine();

        }
    }
}
