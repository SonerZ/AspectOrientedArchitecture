using System;
using System.Threading.Tasks;
using Application.Packages.RabbitMQ.Service;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Application.Packages.RabbitMQ.Subscriber;

public class RabbitMQSubscriberService : IRabbitMQSubscriberService
{
    private IRabbitMQService _RabbitMQService;

    public RabbitMQSubscriberService(IRabbitMQService rabbitMqService)
    {
        _RabbitMQService = rabbitMqService;
    }
    
    public async Task Regiser(string queueName,Func<BasicDeliverEventArgs, bool> ConsumeMethod)
    {
        await Task.Run(() =>
        {
            using var connection = _RabbitMQService.CreateConnection();
            using var channel = _RabbitMQService.CreateModel(connection);

            channel.QueueDeclare(queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (sender, message) =>
            {
                try
                {
                    var result = ConsumeMethod(message);
                    await Task.Yield();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            };
            channel.BasicConsume(queueName, true, consumer);
            while (true)
            {
                Task.Delay(1000 * 5).Wait();
            }
        });
    }
}