using System;
using Application.Entities.CustomEntities;
using Application.Packages.JWT.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Application.WebAPI.Controller;

[ApiController]
[TypeFilter(typeof(ApplicationTokenAuthFilter))]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentManager _departmentManager;
    public DepartmentController(IDepartmentManager departmentManager)
    {
        _departmentManager = departmentManager;
    }
    
    [HttpPost("department")] 
    [ApplicationIgnoreTokenAuthFilter]
    public IActionResult CreateDepartment([FromForm]CreateDepartmentDto departmentDto) 
        => Ok(_departmentManager.CreateDepartment(departmentDto));
    
    [HttpPut("department")] 
    [ApplicationIgnoreTokenAuthFilter]
    public IActionResult UpdateDepartment([FromForm]UpdateDepartmentDto departmentDto) 
        => Ok(_departmentManager.UpdateDepartment(departmentDto));
    
    [HttpDelete("department")] 
    [ApplicationIgnoreTokenAuthFilter]
    public IActionResult RemoveDepartment(Guid key) 
        => Ok(_departmentManager.RemoveDepartment(key));
    
    
    [HttpGet("department/{id}")] 
    [ApplicationIgnoreTokenAuthFilter]
    public IActionResult GetDepartment(string id) 
        => Ok(_departmentManager.GetDepartment(id));
        
    [HttpGet("department")] 
    [ApplicationIgnoreTokenAuthFilter]
    public IActionResult GetDepartments(Guid? function) 
        => Ok(_departmentManager.GetDepartments(function));
}