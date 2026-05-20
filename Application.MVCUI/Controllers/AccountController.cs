using System.Diagnostics;
using Application.Entities.CustomEntities;
using Application.Entities.CustomEntities.User;
using Microsoft.AspNetCore.Mvc;
using Application.MVCUI.Models;
using Application.MVCUI.Services.Session;

namespace Application.MVCUI.Controllers;

public class AccountController : Controller
{
    private readonly IUserManager _userManager;
    private readonly IDepartmentManager _departmentManager;
    private readonly ISessionService _sessionService;


    public AccountController(IUserManager userManager, ISessionService sessionService, IDepartmentManager departmentManager)
    {
        _userManager = userManager;
        _sessionService = sessionService;
        _departmentManager = departmentManager;
    }

    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult Register()
    {
        return View();
    }
    
    public IActionResult Logouth()
    {
        _sessionService.Remove("user-session");
        return RedirectToAction("Index");
    }
    
    public IActionResult User()
    {
        var result = _userManager.GetUsers();
        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Messages[0].Value;
            return RedirectToAction("Index", "Home");
        }

        TempData["Roles"] = _userManager.GetRoles().Data;
        TempData["Claims"] = _userManager.GetClaims().Data;
        
        return View(result.Data);
    }
    
    public IActionResult Role()
    {
        var result = _userManager.GetRoles();
        TempData["Roles"] = _userManager.GetRoles().Data;
        TempData["Claims"] = _userManager.GetClaims().Data;
        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Messages[0].Value;
            return RedirectToAction("Index", "Home");
        }

        return View(result.Data);
    }
    
    public IActionResult Claim()
    {
        var result = _userManager.GetClaims();
        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Messages[0].Value;
            return RedirectToAction("Index", "Home");
        }

        return View(result.Data);
    }
    
    [HttpGet]
    public IActionResult GetRoles()
    {
        var result = _userManager.GetRoles();

        return Ok(result.Data);
    }

    
    public IActionResult Department()
    {
        var result = _userManager.GetUsers();
        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Messages[0].Value;
            return RedirectToAction("Index", "Home");
        }
        return View(result.Data);
    }

    public IActionResult Function()
    {
        return View();
    }
    
    [HttpPost("Register")]
    public IActionResult Register([FromBody]UserCreateDto model)
    {
        var result = _userManager.Create(model);

        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Messages[0].Value;
            return RedirectToAction("Register");
        }
        
        return RedirectToAction("");
    }
    
    [HttpPost()]
    public IActionResult RegisterForm(UserCreateDto model)
    {
        var result = _userManager.Create(model);

        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Messages[0].Value;
            return RedirectToAction("Register");
        }
        
        return RedirectToAction("");
    }
    
    [HttpPost]
    public IActionResult Login(UserLoginDto model)
    {
        var result = _userManager.Login(model);

        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Messages[0].Value;
            return RedirectToAction("Index");
        }
       
        _sessionService.Add("user-session", result.Data);
        
        return RedirectToAction("Index","Home");
    }

    [HttpGet]
    public IActionResult GetUser(string id)
    {
        return Json(string.Empty);
    }

    [HttpDelete]
    public IActionResult DeleteUser(Guid id)
    {
        var result = _userManager.Remove(id);
        return Ok(result);
    }
    
    [HttpPut]
    public IActionResult AddRoleToUser([FromBody]AddUserRoleDto model)
    {
        var result = _userManager.AddUserRole(model);
        return Ok(result);
    }
    
    [HttpPost]
    public IActionResult AddClaimToUser([FromBody]CreateUserClaimDto model)
    {
        var user = _sessionService?.Get<UserDto>("user-session");
        model.UserId = user.Id;
        var result = _userManager.AddUserClaim(model);
        return Ok(result);
    }
    
    [HttpPost]
    public IActionResult AddClaimToRole([FromBody]CreateRoleClaimDto model)
    {
        var result = _userManager.AddRoleClaim(model);
        return Ok(result);
    }
    
    [HttpDelete]
    public IActionResult RemoveClaimToRole([FromBody]CreateRoleClaimDto model)
    {
        var result = _userManager.RemoveRoleClaim(model);
        return Ok(result);
    }


    [HttpGet]
    public IActionResult GetFunctions()
    {
        var result = _departmentManager.GetFunctions();
        if (!result.IsSuccess)
        {
            return Json(new { data = new List<object>(), totalCount = 0 });
        }
        return Json(new { data = result.Data, totalCount = result.Data.Count });
    }
}