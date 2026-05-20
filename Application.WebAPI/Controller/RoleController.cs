using System;
using Application.Entities.CustomEntities;
using Application.Entities.CustomEntities.User;
using Application.Packages.JWT.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Application.WebAPI.Controller;

[ApiController]
[TypeFilter(typeof(ApplicationTokenAuthFilter))]
public class RoleController: ControllerBase
{
    private readonly IActivityManager _activityManager;
    private readonly IUserManager _userManager;
    public RoleController(IActivityManager activityManager, IUserManager userManager)
    {
        _activityManager = activityManager;
        _userManager = userManager;
    }
    
    [HttpPost("role")] 
    public IActionResult AddRole([FromForm]CreateRoleDto request) 
        => Ok(_userManager.AddRole(request));
    
    [HttpPut("role")] 
    public IActionResult UpdateRole([FromForm]UpdateRoleDto request) 
        => Ok(_userManager.UpdateRole(request));
    
    [HttpDelete("role")] 
    public IActionResult RemoveRole(Guid key) 
        => Ok(_userManager.RemoveRole(key));
    
    [HttpGet("role")] 
    public IActionResult Roles() 
        => Ok(_userManager.GetRoles());
    
    [HttpGet("claim")] 
    public IActionResult Claims() 
        => Ok(_userManager.GetClaims());
    
    [HttpDelete("claim")] 
    public IActionResult RemoveClaim(Guid key) 
        => Ok(_userManager.RemoveClaim(key));
    
    [HttpPut("claim")] 
    public IActionResult UpdateClaim([FromForm]UpdateClaimDto request) 
        => Ok(_userManager.UpdateClaim(request));
    
    [HttpPost("claim")] 
    public IActionResult AddClaim([FromForm]CreateClaimDto request) 
        => Ok(_userManager.AddClaim(request));
    
    [HttpPost("user-claim")] 
    public IActionResult AddUserClaim([FromForm]CreateUserClaimDto request) 
        => Ok(_userManager.AddUserClaim(request));
    
    [HttpDelete("user-claim")] 
    public IActionResult RemoveUserClaim([FromForm]CreateUserClaimDto request) 
        => Ok(_userManager.RemoveUserClaim(request));
}