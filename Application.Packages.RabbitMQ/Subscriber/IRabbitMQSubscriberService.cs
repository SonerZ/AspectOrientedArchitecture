using System;
using System.Threading.Tasks;
using RabbitMQ.Client.Events;

namespace Application.Packages.RabbitMQ.Subscriber;

public interface IRabbitMQSubscriberService
{
    /// <summary>
    /// Insert list of object to RabbitMQ queue.
    /// </summary>
    /// <typeparam name="T">Object type</typeparam>
    /// <param name="listOfData">List of T, T must be reference type</param>
    /// <param name="queueName">RabbitMQ queue name</param>
    Task Regiser(string queueName, Func<BasicDeliverEventArgs, bool> consumeMethod);
}