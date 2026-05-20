using Application.Packages.RabbitMQ;
using Application.Packages.RabbitMQ.Subscriber;
using Application.WebAPI.MQTT.Subscribers.Concrate;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Application.WebAPI.MQTT.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddSubscribers(this IServiceCollection services)
    {
        services.AddSingleton<LogSystemSubscriber>();
    }

    public static void UseSubscribers(this IApplicationBuilder app)
    {
        var rabbitMQSubscriberService = app.ApplicationServices.GetService<IRabbitMQSubscriberService>();
        #region Process Subscriber
        var instanceService = app.ApplicationServices.GetRequiredService<LogSystemSubscriber>();
        rabbitMQSubscriberService.Regiser(QueueConst.LogSystem, instanceService.LogSystem);
        #endregion
    }
}