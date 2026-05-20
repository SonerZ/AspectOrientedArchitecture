using System;
using System.Threading.Tasks;
using Application.Entities.CustomEntities.User;
using Application.Packages.JWT.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.WebAPI.Controller;

[ApiController]
[Route("/user")]
[TypeFilter(typeof(ApplicationTokenAuthFilter))]

public class UserController : ControllerBase
{
    private readonly IUserManager _userManager;
    public UserController(IUserManager userManager)
    {
        _userManager = userManager;
    }
	
    [HttpGet]
    [Route("login")]
    [ApplicationIgnoreTokenAuthFilter]
    public IActionResult Login([FromQuery]UserLoginDto request)
    {
        var checkUserLoginRequest = _userManager.Login(request);

        if (!checkUserLoginRequest.IsSuccess)
        {
            return BadRequest(checkUserLoginRequest);
        }

        return Ok(checkUserLoginRequest);
    }
    
    [HttpPost] 
    [ApplicationIgnoreTokenAuthFilter]
    public IActionResult Create(UserCreateDto user) 
        => Ok(_userManager.Create(user));
    
    [HttpGet("loginned-user")]
    public async Task<IActionResult> AuthenticatedUser() =>
        Ok(await _userManager.AuthenticatedUser());
    
    [HttpGet("/{username}")] 
    public IActionResult GetUser(string username) 
        => Ok(_userManager.GetUser(username));
    
    [HttpPost("user-role")] 
    public IActionResult AddUserRole(AddUserRoleDto request) 
        => Ok(_userManager.AddUserRole(request));
    
    [HttpDelete("user-role")] 
    public IActionResult RemoveUserRole(RemoveUserRoleDto request) 
        => Ok(_userManager.RemoveUserRole(request));
    
    [HttpGet("user-roles")] 
    public IActionResult UserRoles(Guid userId) 
        => Ok(_userManager.UserRoles(userId));
    
    [HttpGet("users")] 
    public IActionResult Users() 
        => Ok(_userManager.GetUsers());
}