using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Application.MVCUI.Models;
using Application.MVCUI.Services.Session;

namespace Application.MVCUI.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ISessionService _sessionService;

    public HomeController(ISessionService sessionService)
    {
        _sessionService = sessionService;
    }

    public IActionResult Index()
    {
        var checkSession = _sessionService.Any("user-session");
        if (!checkSession) return Redirect("Account");
        
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}