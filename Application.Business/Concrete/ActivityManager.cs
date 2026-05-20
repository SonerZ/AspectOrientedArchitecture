using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Core.Constants.Messages;
using Application.Core.Entities.Concrete;
using Application.Core.Utilities.Result;
using Application.DataAccess.Abstract;
using Application.Entities.CustomEntities.User;
using AutoMapper;

namespace Application.Business.Concrete;

public class ActivityManager : IActivityManager
{
    private readonly IMapper _mapper;
    private readonly IActivityDal _activityDal;
    private readonly IUserManager _userManager;

    public ActivityManager(IMapper mapper, IActivityDal activityDal, IUserManager userManager)
    {
        _mapper = mapper;
        _activityDal = activityDal;
        _userManager =userManager;
    }
   
    public IResult Create(ActivityCreateDto activity)
    {
        var authUser = _userManager.AuthenticatedUser()
        .GetAwaiter().GetResult();

        if(!authUser.IsSuccess)
             return new ErrorResult(ResultMessages.NotFound);

        var entity = _mapper.Map<Activity>(activity);

        entity.UserId = authUser.Data.Id;

        var isAdded = _activityDal.Add(entity, authUser.Data.Username);

        if(!isAdded)
            return new ErrorResult(ResultMessages.NotBeAdded);

        return new SuccessResult();
    }
    
    public IResult Update(ActivityUpdateDto activity)
    {
        var authUser = _userManager.AuthenticatedUser()
            .GetAwaiter().GetResult();

        if(!authUser.IsSuccess)
            return new ErrorResult(ResultMessages.NotFound);

        var entity =_activityDal.Find(i=> i.Id == activity.Id);

        if (entity is null)
        {
            return new ErrorResult();
        }

        entity.UserId = authUser.Data.Id;
        
        entity.CheckValue = activity.Check;
        
        entity.Description = activity.Description;
        entity.Count = activity.Count;
        entity.ActivityType = activity.ActivityType;

        var isAdded = _activityDal.Update(entity, authUser.Data.Username);

        if(!isAdded)
            return new ErrorResult(ResultMessages.NotBeAdded);

        return new SuccessResult();
    }
    
    public IResult Remove(Guid id)
    {
        var authUser = _userManager.AuthenticatedUser()
            .GetAwaiter().GetResult();

        if(!authUser.IsSuccess)
            return new ErrorResult(ResultMessages.NotFound);

        var entity =_activityDal.Find(i=> i.Id == id);

        if (entity is null)
        {
            return new ErrorResult();
        }

        var isAdded = _activityDal.Remove(entity);

        if(!isAdded)
            return new ErrorResult(ResultMessages.NotBeAdded);

        return new SuccessResult();
    }

    public  IDataResult<IEnumerable<ActivityDto>> GetActivities()
    {
       var activities =  _activityDal.GetActivitiesFullInclude()
           .GetAwaiter().GetResult();

       var activityResp = _mapper.Map<IEnumerable<ActivityDto>>(activities);

       return new SuccessDataResult<IEnumerable<ActivityDto>>(activityResp);
    }

    public IDataResult<ActivityBaseDto> GetActivity(string id)
    {
        var activity = _activityDal.Find(i=> i.Id == Guid.Parse(id));

        if(activity is null)
            return new ErrorDataResult<ActivityBaseDto>(ResultMessages.NotFound);

        var activityResp = _mapper.Map<ActivityBaseDto>(activity);

        return new SuccessDataResult<ActivityBaseDto>(activityResp);
    }
}