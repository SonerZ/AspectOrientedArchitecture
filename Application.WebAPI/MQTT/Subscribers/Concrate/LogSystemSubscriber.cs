using Application.WebAPI.MQTT.Subscribers.Abstract;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System.Text;
using Application.Core.Configuration.Context;
using Application.Packages.HttpClientService;
using Application.Packages.RabbitMQ.RabbitMQModels;

namespace Application.WebAPI.MQTT.Subscribers.Concrate;

public class LogSystemSubscriber : ISubscriber
{
    private readonly IApplicationConfigurationContext _applicationConfigurationContext;
    private readonly IHttpService _httpService;
    public LogSystemSubscriber(IApplicationConfigurationContext configurationContext , IHttpService httpService)
    {
        _applicationConfigurationContext = configurationContext;
        _httpService = httpService;
    }
    public bool LogSystem(BasicDeliverEventArgs message)
    {
        var logSystemModel = JsonConvert.DeserializeObject<LogSystem>(Encoding.Default.GetString(message.Body.ToArray()));

        return true;
    }
}