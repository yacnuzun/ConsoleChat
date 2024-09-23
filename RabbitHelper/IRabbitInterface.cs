using RabbitHelper.ResponseModels;
using RabbitMQ.Client;

namespace RabbitHelper
{
    public interface IRabbitInterface
    {
        public BaseResponse<ReponseModel> SendMessage(IModel model, int contactID, string consoleMessage, string contactName, string queque);

    }
}
