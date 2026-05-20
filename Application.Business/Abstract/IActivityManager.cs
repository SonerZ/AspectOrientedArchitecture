using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Business.ValidationRules.FluentValidation;
using Application.Core.Constants;
using Application.Core.Utilities.Result;
using Application.Entities.CustomEntities.User;
using Application.Packages.AOP.Aspects.Caching;
using Application.Packages.AOP.Aspects.Exception;
using Application.Packages.AOP.Aspects.Performance;
using Application.Packages.AOP.Aspects.Secure;
using Application.Packages.AOP.Aspects.Validation;

public interface IActivityManager
{
   
    [ExceptionAspect(Priority =4)]
    [ValidationAspect<IResult>(typeof(ActivityValidator), Priority =2)]
    [CacheRemoveAspect("IActivityManager", Priority = 3)]
    [SecuredOperationAspect<IResult>("Create, Update", Priority = 1)]
    IResult Create(ActivityCreateDto Activity);
    
    [ExceptionAspect(Priority =4)]
    [CacheRemoveAspect("IActivityManager", Priority = 3)]
    [SecuredOperationAspect<IResult>("Update", Priority = 1)]
    IResult Update(ActivityUpdateDto Activity);
    
    [ExceptionAspect(Priority =4)]
    [CacheRemoveAspect("IActivityManager", Priority = 3)]
    [SecuredOperationAspect<IResult>("Remove", Priority = 1)]
    IResult Remove(Guid id);

    [PerformanceAspect(1000, Priority = 4)]
    [ExceptionAspect(Priority = 3)]
    [CacheAspect<IEnumerable<ActivityDto>>(Priority = 2)]
    [SecuredOperationAspect<IEnumerable<ActivityDto>>("Admin", Priority = 1)]
    IDataResult<IEnumerable<ActivityDto>> GetActivities();

    [ExceptionAspect]
    [ValidationAspect<IDataResult<ActivityBaseDto>>(typeof(BaseRequestDtoValidator))]
    [SecuredOperationAspect<IDataResult<ActivityBaseDto>>("Admin", Priority = 1)]
    IDataResult<ActivityBaseDto> GetActivity(string id);
}