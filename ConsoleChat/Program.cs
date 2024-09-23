using RabbitHelper;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;


string contactMessage = "";
string contactName = "Consumer2";
int contactID = 2;
bool isFinish = false;
string queque = "message";
Console.WriteLine(contactName);
RabbitImplementations rabbitInterface = new RabbitImplementations();

var factory = new ConnectionFactory() { HostName = "localhost" };
using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    channel.QueueDeclare(queue: "message2",
                         durable: false,
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);

    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine(" [Consumer1] Received {0}", message, contactName);
    };

    channel.BasicConsume(queue: "message2",
                         autoAck: true,
                         consumer: consumer);


    while (isFinish != true)
    {
        contactMessage = Console.ReadLine();

        rabbitInterface.SendMessage(channel,contactID, contactMessage, contactName, queque);

    }
}
