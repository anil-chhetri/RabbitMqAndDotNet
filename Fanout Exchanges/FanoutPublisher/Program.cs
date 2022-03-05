using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;

namespace FanoutPublisher
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectionFactory factory = new ConnectionFactory()
            {
                HostName = "Localhost",
                VirtualHost = "FanOutExchanges",
                UserName = "anil",
                Password = "anil",
                Port = 5672
            };

            var conn = factory.CreateConnection();
            var channel = conn.CreateModel();

            //creating exchanges.
            // create fanout exchange with ex.fanout name.
            channel.ExchangeDeclare("ex.fanout", "fanout", true, false, null);

            //creating queue
            channel.QueueDeclare("my.queue1", true, false, false, null);

            channel.QueueDeclare("my.queue2", true, false, false, null);

            //binding exchange and queue
            //even though we don't need routing key for fanout keep blank rather than null.
            channel.QueueBind("my.queue1", "ex.fanout", "", null);

            channel.QueueBind("my.queue2", "ex.fanout", "", null);

            //sending message to queue.
            channel.BasicPublish("ex.fanout", "", null, Encoding.ASCII.GetBytes("Message 1"));
            channel.BasicPublish("ex.fanout", "", null, Encoding.ASCII.GetBytes("Message 2"));

            System.Console.WriteLine("Message published. press anykey to exit.");
            Console.ReadKey();

            //cleaning up
            channel.QueueDelete("my.queue1");
            channel.QueueDelete("my.queue2");

            channel.ExchangeDelete("ex.fanout");

            channel.Close();
            conn.Close();



        }
    }
}
