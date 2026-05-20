using System;
using System.Dynamic;
using System.Net;
using Application.Core.Configuration.Context;
using Application.Core.Utilities.DependencyServiceTool;
using Application.Packages.AOP.Helpers;
using Application.Packages.AOP.Interceptor;
using Application.Packages.Caching.Core.Service;
using Application.Packages.HttpClientService;
using Application.Packages.RabbitMQ;
using Application.Packages.RabbitMQ.Publisher;
using Application.Packages.RabbitMQ.RabbitMQModels;
using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Packages.AOP.Aspects.Exception
{
    /// <summary>
    /// ExceptionAspect works when method is throwing exception
    /// </summary>
    public class ExceptionAspect : MethodInterceptor
    {
        private IRabbitMQPublisherService _publisherService;
        private readonly IHttpService _httpService;
        private readonly IApplicationConfigurationContext _applicationConfigurationContext;
        private readonly ICacheService _cacheService;
        public ExceptionAspect() 
        {
             _publisherService = (IRabbitMQPublisherService)
                DependencyServiceTool.ServiceProvider.GetRequiredService(typeof(IRabbitMQPublisherService));
            
            _httpService =  (IHttpService)DependencyServiceTool.ServiceProvider.GetRequiredService(typeof(IHttpService));
            
            _applicationConfigurationContext =  (IApplicationConfigurationContext)DependencyServiceTool.ServiceProvider.GetRequiredService(typeof(IApplicationConfigurationContext));
            
            var service = DependencyServiceTool.ServiceProvider.GetService(typeof(ICacheService));

            _cacheService = (ICacheService)service;
        }
        
        public override void OnException(IInvocation invocation, System.Exception exception)
        {
            var hostName = Dns.GetHostName();
            
            var enqueueData = new LogSystem
            {
                Content = exception.Message,
                Level = QueueConst.Error,
                Name = QueueConst.InternetServerError,
                LogDate = DateTime.UtcNow,
                ServerIp = Dns.GetHostByName(hostName).AddressList[0]?.ToString(),
                ServerName = hostName,
                UserId = QueueConst.UserId,
                Func = Convert.ToString(invocation).Split(".")[3]
            };
            
            var key = $"{AspectHelper.GetMethodKey(invocation: invocation)}-{QueueConst.Error}";
        
            if (!_cacheService.Any(key))
            {
                _cacheService.Add(key, QueueConst.Error , 9999);
                
                _publisherService.Enqueue(enqueueData, QueueConst.LogSystem);
            }
        }
        
        public override void OnSuccess(IInvocation invocation)
        {
            var key = $"{AspectHelper.GetMethodKey(invocation: invocation)}-{QueueConst.Error}";
            
            if (_cacheService.Any(key))
            {
                dynamic model = new ExpandoObject();
                
                model.func = Convert.ToString(invocation).Split(".")[3];
               
                _publisherService.Enqueue(model, QueueConst.LogSystem);
                _cacheService.Remove(key);
            }
        }
    }
}
