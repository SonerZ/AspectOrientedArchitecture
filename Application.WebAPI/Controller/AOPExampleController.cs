using System;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using Application.Business.Abstract;
using DefaultNamespace;
using Microsoft.AspNetCore.Mvc;

namespace Application.WebAPI.Controller;

[ApiController]
[Route("/cross-cutting-concerns")]
public class AOPExampleController : ControllerBase
{
    private readonly IAOPExampleManager _aopExampleManager;

    public AOPExampleController(IAOPExampleManager exampleManager)
    {
       
        _aopExampleManager = exampleManager;
    }
    
    [HttpPost("error-test")] public IActionResult Error() => Ok(_aopExampleManager.ErrorTest());

    [HttpGet("performance-test")]
    public async Task<IActionResult> Performance()
    {
       var result = await _aopExampleManager.PerformanceTest();

       return new AcceptedResult();
    }
    
    [HttpPost("validation-test")] public IActionResult Validation(BaseIdDTO dto) => Ok(_aopExampleManager.ValidationTest(dto));   
    
    [HttpGet("cache-test")] public IActionResult Cache() => Ok(_aopExampleManager.CacheTest());
    
    [HttpGet("clear-cache-test")] public IActionResult ClearCache() => Ok(_aopExampleManager.ClearCacheTest());
}