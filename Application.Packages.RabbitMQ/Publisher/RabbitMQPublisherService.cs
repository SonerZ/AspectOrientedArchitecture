using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Packages.RabbitMQ.RabbitMQModels;
using Application.Packages.RabbitMQ.Service;
using Newtonsoft.Json;

namespace Application.Packages.RabbitMQ.Publisher
{
    public class RabbitMQPublisherService : IRabbitMQPublisherService
    {
        private IRabbitMQService _RabbitMQService;

        public RabbitMQPublisherService(IRabbitMQService rabbitMqService)
        {
            _RabbitMQService = rabbitMqService;
        }

        public void Enqueue<T>(T data, string queueName) where T : class, IRabbitMQBaseModel ,new()
        {
            using var connection = _RabbitMQService.CreateConnection();
            using var channel = _RabbitMQService.CreateModel(connection);

            channel.QueueDeclare(queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);


            var jsonData = JsonConvert.SerializeObject(data);
            var body = Encoding.UTF8.GetBytes(jsonData);

            channel.BasicPublish(exchange: "", routingKey: queueName, mandatory: false, basicProperties: null, body: body);
        }

        public void EnqueueList<T>(IEnumerable<T> listOfData, string queueName) where T : class, new()
        {
            using var connection = _RabbitMQService.CreateConnection();
            using var channel = _RabbitMQService.CreateModel(connection);

            channel.QueueDeclare(queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            listOfData.ToList().ForEach(data =>
            {
                var jsonData = JsonConvert.SerializeObject(data);
                var body = Encoding.UTF8.GetBytes(jsonData);

                channel.BasicPublish(exchange: "", routingKey: queueName, mandatory: false, basicProperties: null, body: body);
            });
        }
    }
}
