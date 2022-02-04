using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory() { 
    HostName = "Localhost",
    UserName = "anil",
    Password = "anil",
    VirtualHost = "test",
    Port = 5672
 };
 factory.AutomaticRecoveryEnabled = true;
 factory.DispatchConsumersAsync = true;
 

 using(var connection = factory.CreateConnection("Sender"))
 using(var channel = connection.CreateModel())
 {
    /*
    queue: name of the queueu
    durable: the queue will survive a broker restart
    exclusive: used by only one connection and 
         the queue will be deleted when the connectino closes.
    autodelete: queue that has had at least one consumer is deleted when last
         consumer unsubscribes
    arguments: used by plugins and broker specific features such as message TTL,
       queue length limit

       https://www.rabbitmq.com/queues.html
    */
    channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);
    
    string message = "Hello world";

    var body = Encoding.UTF8.GetBytes(message);

    channel.BasicPublish(exchange: "", routingKey: "hello", basicProperties: null, body: body );

    System.Console.WriteLine($"[x] sent {message}");
 }

 System.Console.WriteLine("press [enter to exit:");
 Console.ReadLine();
 
    //  System.Console.WriteLine("Checking connections.");
    //  Console.ReadLine();
