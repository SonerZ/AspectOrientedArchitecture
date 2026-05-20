using System;
using System.Diagnostics;
using System.Net;
using Application.Core.Configuration.Context;
using Application.Core.Configuration.Environment;
using Application.Core.Utilities.DependencyServiceTool;
using Application.Packages.AOP.Interceptor;
using Application.Packages.RabbitMQ;
using Application.Packages.RabbitMQ.Publisher;
using Application.Packages.RabbitMQ.RabbitMQModels;
using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;


namespace Application.Packages.AOP.Aspects.Performance;

public class PerformanceAspect : MethodInterceptor
{
    private int _interval;
    private Stopwatch _stopwatch;
    private  IRabbitMQPublisherService _publisherService { get; set; }
    private readonly IApplicationConfigurationContext _applicationConfigurationContext;

    public PerformanceAspect(int interval)
    {
        _interval = interval;
        _stopwatch = new Stopwatch();
        var service = DependencyServiceTool.ServiceProvider.GetService(typeof(IRabbitMQPublisherService));

        _publisherService = (IRabbitMQPublisherService)service;
        
        _applicationConfigurationContext =  (IApplicationConfigurationContext)DependencyServiceTool.ServiceProvider.GetRequiredService(typeof(IApplicationConfigurationContext));        
    }
    public override void OnBefore(IInvocation invocation)
    {
        _stopwatch.Start();
    }

    public override void OnAfter(IInvocation invocation)
    {
        if (_stopwatch.Elapsed.TotalSeconds>_interval)
        {
            var content =
                $"Performance : {invocation.Method.DeclaringType.FullName}.{invocation.Method.Name}-->{_stopwatch.Elapsed.TotalSeconds}";

            var hostName = Dns.GetHostName();
            
            var enqueueData = new LogSystem
            {
                Content = content,
                Level = QueueConst.Performance,
                Name = $"Performance : {invocation.Method.DeclaringType.Name}",
                LogDate = DateTime.UtcNow,
                ServerIp = Dns.GetHostByName(hostName).AddressList[0]?.ToString(),
                ServerName = hostName,
                UserId = QueueConst.UserId
            };
            
            _publisherService.Enqueue(enqueueData, QueueConst.LogSystem);
        }
        
        _stopwatch.Reset();
    }
}