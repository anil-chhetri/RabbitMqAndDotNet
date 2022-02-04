using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory()
{
    HostName = "localhost",
    UserName = "anil",
    Password = "anil",
    VirtualHost = "test"
};


using(var connection = factory.CreateConnection("consumer"))
using(var model = connection.CreateModel())
{
    //should match with the queue that sender publish
    model.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);

    var consumer = new EventingBasicConsumer(model);

    consumer.Received += (model, ea) => {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine($"[x] recived {message}");
    };

    model.BasicConsume(queue: "hello", autoAck: true, consumer: consumer);

    System.Console.WriteLine("press [enter] to exit.");
    Console.ReadLine();


}
