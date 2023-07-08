using E_CommerceForUdemy_DataAccess;

namespace E_CommerceForUdemy_Business.RabbitMQSender
{
    public interface IRabbitMQMessageSender
    {
        void SendMessage(BaseMessage baseMessage, String queueName);
    }
}
