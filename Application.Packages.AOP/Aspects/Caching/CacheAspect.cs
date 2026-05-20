using System.Threading.Tasks;
using Application.Core.Utilities.DependencyServiceTool;
using Application.Core.Utilities.Result;
using Application.Packages.AOP.Helpers;
using Application.Packages.AOP.Interceptor;
using Application.Packages.Caching.Core.Service;
using Castle.DynamicProxy;
using Newtonsoft.Json;

namespace Application.Packages.AOP.Aspects.Caching;

public class CacheAspect<T> : MethodInterceptor
    where T : class
{
    private int _timeout;
    private readonly ICacheService _cacheService;

    public CacheAspect(int timeout = 60)
    {
        _timeout = timeout;
        var service = DependencyServiceTool.ServiceProvider.GetService(typeof(ICacheService));

        _cacheService = (ICacheService)service;
    }

    public override void Intercept(IInvocation invocation)
    {
        var key = AspectHelper.GetMethodKey(invocation: invocation);

        if (_cacheService.Any(key))
        {
            invocation.ReturnValue = _cacheService.Get<object>(key);

            return;
        }

        invocation.Proceed();
        
        if (invocation.ReturnValue == default)
        {
            return;
        }

        var methdReturnType = invocation.MethodInvocationTarget.ReturnType;

        if (methdReturnType == typeof(IDataResult<T>))
        {
            var jsonData = JsonConvert.SerializeObject(invocation.ReturnValue);
            
            var dataResult = JsonConvert.DeserializeObject<DataResult<T>>(jsonData);

            dataResult.IsCache = true;

            _cacheService.Add(key, dataResult, _timeout);

            return;
        }

        if (methdReturnType == typeof(Task<IDataResult<T>>))
        {
            Task<IDataResult<T>> taskData = (Task<IDataResult<T>>)invocation.ReturnValue;
           
            IDataResult<T> clonedData = new DataResult<T>
            {
                Data = taskData.Result.Data,
                IsCache = true,
                Messages = taskData.Result.Messages,
                IsSuccess = taskData.Result.IsSuccess
            }; 
            
            Task<IDataResult<T>> clonedTask = Task.FromResult(clonedData);

            _cacheService.Add(key, clonedTask, _timeout);
        }
    }
}