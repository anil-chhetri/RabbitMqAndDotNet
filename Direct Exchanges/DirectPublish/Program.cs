using System;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace DirectPublish
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Publish started.");
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
                VirtualHost = "DirectExchange",
                UserName = "anil",
                Password = "anil"
            };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();


            channel.ExchangeDeclare("DirectExchange", "direct", true, false, null);

            channel.QueueDeclare("info", true, false, false, null);
            channel.QueueDeclare("warning", true, false, false, null);
            channel.QueueDeclare("error", true, false, false, null);

            channel.QueueBind("info", "DirectExchange", "infokey", null);
            channel.QueueBind("error", "DirectExchange", "errorkey", null);
            channel.QueueBind("warning", "DirectExchange", "warningkey", null);


            channel.BasicPublish("DirectExchange", "errorkey", false, null, Encoding.UTF8.GetBytes("{'error': 'there is error in the process'}"));
            channel.BasicPublish("DirectExchange", "infokey", false, null, Encoding.UTF8.GetBytes("{'error': 'there is information in the process'}"));
            channel.BasicPublish("DirectExchange", "warningkey", false, null, Encoding.UTF8.GetBytes("{'error': 'there is warning in the process'}"));

            System.Console.WriteLine("message published");
            System.Console.WriteLine("press any key to exit");
            System.Console.ReadKey();

            channel.QueueDelete("info", false, false);
            channel.QueueDelete("error", false, false);
            channel.QueueDelete("warning", false, false);

            channel.ExchangeDelete("DirectExchange", false);

            channel.Close();

            connection.Close();





        }
    }
}
