using System;
using Application.Entities.CustomEntities.User;
using Application.Packages.JWT.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Application.WebAPI.Controller;

[ApiController]
[TypeFilter(typeof(ApplicationTokenAuthFilter))]
public class ActivityController: ControllerBase
{
    private readonly IActivityManager _activityManager;
    public ActivityController(IActivityManager activityManager)
    {
        _activityManager = activityManager;
    }
    
    [HttpPost("activity")] 
    public IActionResult Create([FromBody]ActivityCreateDto model) 
        => Ok(_activityManager.Create(model));
    
    [HttpPut("activity")] 
    public IActionResult Update([FromBody]ActivityUpdateDto model) 
        => Ok(_activityManager.Update(model));
    
    [HttpDelete("activity")] 
    public IActionResult Remove(Guid id) 
        => Ok(_activityManager.Remove(id));
    
    [HttpGet("activity")] 
    public IActionResult GetActivities() 
        => Ok(_activityManager.GetActivities());
    
    [HttpGet("get-activity")] 
    public IActionResult Activity(Guid id) 
        => Ok(_activityManager.GetActivity(id.ToString()));
}