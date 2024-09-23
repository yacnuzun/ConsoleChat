using RabbitHelper.ResponseModels;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitHelper
{
    public class RabbitImplementations : IRabbitInterface
    {
        public BaseResponse<ReponseModel> SendMessage(IModel model, int contactID, string consoleMessage, string contactName, string queque)
        {
            
                model.QueueDeclare(queue: queque,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false);

                string message = consoleMessage;
                var body = Encoding.UTF8.GetBytes(message);

                model.BasicPublish(exchange: "",
                                     routingKey: queque,
                                     body: body);

                return new BaseResponse<ReponseModel> { Body = new ReponseModel { ContactID = contactID, ContactName = contactName, Message = consoleMessage, MessageID = 1 } };

        }
    }
}
