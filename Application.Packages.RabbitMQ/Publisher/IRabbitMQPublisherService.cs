using System;
using System.Collections.Generic;
using System.Text;
using Application.Packages.RabbitMQ.RabbitMQModels;

namespace Application.Packages.RabbitMQ.Publisher
{
    /// <summary>
    /// This interface provides publishing list of data to RabbitMQ queue.
    /// </summary>
    public interface IRabbitMQPublisherService
    {
        /// <summary>
        /// Insert list of object to RabbitMQ queue.
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="listOfData">List of T, T must be reference type</param>
        /// <param name="queueName">RabbitMQ queue name</param>
        void EnqueueList<T>(IEnumerable<T> listOfData, string queueName) where T : class, new();

        void Enqueue<T>(T data, string queueName) where T : class,IRabbitMQBaseModel, new();
    }
}
