using System;
using Application.Entities.CustomEntities;
using Application.Packages.JWT.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Application.WebAPI.Controller;

[ApiController]
[TypeFilter(typeof(ApplicationTokenAuthFilter))]

public class FunctionController : ControllerBase
{
    private readonly IDepartmentManager _departmentManager;
    public FunctionController(IDepartmentManager departmentManager)
    {
        _departmentManager = departmentManager;
    }
    
    [HttpPost("function")] 
    [ApplicationIgnoreTokenAuthFilter]
    public IActionResult CreateFunction([FromForm]CreateFunctionDto functionDto) 
        => Ok(_departmentManager.CreateFunction(functionDto));
    
    [HttpPut("function")] 
    [ApplicationIgnoreTokenAuthFilter]
    public IActionResult UpdateFunction([FromForm]UpdateFunctionDto functionDto) 
        => Ok(_departmentManager.UpdateFunction(functionDto));
    
    [HttpDelete("function")] 
    [ApplicationIgnoreTokenAuthFilter]
    public IActionResult RemoveFunction(Guid key) 
        => Ok(_departmentManager.RemoveFunction(key));
    
    
    [HttpGet("function")] 
    [ApplicationIgnoreTokenAuthFilter]
    public IActionResult GetFunctions()
    { 
        return Ok(_departmentManager.GetFunctions().Data);
    }
}