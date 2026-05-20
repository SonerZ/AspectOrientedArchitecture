using System.Diagnostics;
using System.Threading.Tasks;
using Application.Business.Abstract;
using Application.Business.ValidationRules.FluentValidation;
using Application.Core.Utilities.Result;
using Application.Packages.AOP.Aspects.Caching;
using Application.Packages.AOP.Aspects.Exception;
using Application.Packages.AOP.Aspects.Performance;
using Application.Packages.AOP.Aspects.Validation;
using AutoMapper;
using DefaultNamespace;

namespace Application.Business.Concrete;

public class AOPExampleManager : IAOPExampleManager
{
    private readonly IMapper _mapper;

    public AOPExampleManager(IMapper mapper)
    {
        _mapper = mapper;
    }

    [ExceptionAspect]
    public IDataResult<string> ErrorTest()
    {
        var a = 5;
        var b = 0;

        var result = a / b;
        
        return new SuccessDataResult<string>("Method 500 sunucu hatasına düşürüldü.");
    }  
    
    [ValidationAspect<BaseIdDTO>(typeof(BaseRequestDtoValidator))]
    public IDataResult<BaseIdDTO> ValidationTest(BaseIdDTO request)
    {
        return new SuccessDataResult<BaseIdDTO>(request);
    }  
    
    [PerformanceAspect(2)]
    public async Task<IDataResult<string>> PerformanceTest()
    {
        int delay = 5500;
        
        Task.Delay(delay).GetAwaiter().GetResult();

        return new SuccessDataResult<string>($"Method {delay}ms süre geciktirildi.");
    }

   
    [CacheAspect<string>(Priority = 3)]
    [ExceptionAspect(Priority = 2)]
    [PerformanceAspect(2,Priority = 1)] 
    public IDataResult<string> CacheTest()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        int totalCount = 1;
        while (stopwatch.ElapsedMilliseconds < 10000)
        {
            totalCount += 1;
        }

        stopwatch.Stop();

        return new SuccessDataResult<string>($"Hesaplanan Sonuç: {totalCount}");
    }
    
    [CacheRemoveAspect("IAOPExampleManager")]
    public IResult ClearCacheTest()
    {
        return new SuccessResult();
    }
}