using Microsoft.Extensions.Hosting;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_CommerceForUdemy_Business.Core;
using E_CommerceForUdemy_DataAccess;
using Newtonsoft.Json;

namespace E_CommerceForUdemy_Business.Consumer
{
    public class RabbitMQOrderConsumer : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private const string EchangeName = "PublishSubscribePaymentUpdate_Exchange";
        private readonly IMailHelper _emailSender;
        string queueName = "";
        public RabbitMQOrderConsumer(IMailHelper emailSender)
        {
            _emailSender = emailSender;
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(EchangeName, ExchangeType.Fanout);
            queueName = _channel.QueueDeclare().QueueName;
        }



        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                OrderHeaderDTO order = JsonConvert.DeserializeObject<OrderHeaderDTO>(content);
                HandleMessage(order).GetAwaiter().GetResult();
                _channel.BasicAck(ea.DeliveryTag, false);
            };
            _channel.BasicConsume("succesProcessTopic", false, consumer);
            return Task.CompletedTask;
        }

        private async Task HandleMessage(OrderHeaderDTO order)
        {
            try
            {
                 _emailSender.SendEmailForOrder("veznedaroglufayik2@gmail.com", "denee", order.Email); ;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
