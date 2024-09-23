using RabbitHelper;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;


var factory = new ConnectionFactory() { HostName = "localhost" };
string contactMessage = "";
string contactName = "Consumer1";
int contactID = 1;
string queque = "message2";
bool isFinish = false;
Console.WriteLine(contactName);
RabbitImplementations rabbitInterface = new RabbitImplementations();

using (var connection = factory.CreateConnection())
{
    using (var channel = connection.CreateModel())
    {
        channel.QueueDeclare(queue: "message",
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        var consumer = new EventingBasicConsumer(channel);
        channel.BasicConsume(queue: "message",
                             autoAck: true,
                             consumer: consumer);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine(" [Consumer2] Received {0}", message);
        };

        
        while (isFinish != true)
        {
            contactMessage = Console.ReadLine();

            rabbitInterface.SendMessage(channel,contactID, contactMessage, contactName, queque);

        }

    }
    contactMessage = Console.ReadLine();

}


