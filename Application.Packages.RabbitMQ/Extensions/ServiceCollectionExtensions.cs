using System;
using System.Collections.Generic;
using System.Text;
using Application.Packages.RabbitMQ.Configuration;
using Application.Packages.RabbitMQ.Publisher;
using Application.Packages.RabbitMQ.Service;
using Application.Packages.RabbitMQ.Subscriber;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Packages.RabbitMQ.Extensions
{
    /// <summary>
    /// ServiceCollectionExtensions contains extended IServiceCollection's methods.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Register in RabbitMQ dependencies to IServiceCollection
        /// </summary>
        /// <returns></returns>
        public static IServiceCollection AddRabbitMQ(this IServiceCollection services, Action<IRabbitMQConfiguration> configuration)
        {
            IRabbitMQConfiguration rabbitMqConfiguration = new RabbitMQConfiguration();
            configuration.Invoke(rabbitMqConfiguration);

            services.AddSingleton<IRabbitMQConfiguration>(rabbitMqConfiguration);

            services.AddSingleton<IRabbitMQService, RabbitMQService>();

            services.AddSingleton<IRabbitMQPublisherService, RabbitMQPublisherService>();
            
            services.AddSingleton<IRabbitMQSubscriberService, RabbitMQSubscriberService>();

            return services;
        }
    }
}
