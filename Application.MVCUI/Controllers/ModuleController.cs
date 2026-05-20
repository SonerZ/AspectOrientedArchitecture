using System.Diagnostics;
using Application.Entities.CustomEntities.User;
using Microsoft.AspNetCore.Mvc;
using Application.MVCUI.Models;
using Application.MVCUI.Services.Session;

namespace Application.MVCUI.Controllers;

public class ModuleController : Controller
{
    private readonly IUserManager _userManager;
    private readonly IActivityManager _activityManager;
    private readonly ISessionService _sessionService;

    public ModuleController(IUserManager userManager, ISessionService sessionService, IActivityManager activityManager)
    {
        _userManager = userManager;
        _sessionService = sessionService;
        _activityManager = activityManager;
    }

    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult Create()
    {
        return View();
    }
    
    public IActionResult Edit(Guid id)
    {
        if (id == default)
        {
            return View("Index");
        }
        
        return View();
    }
}