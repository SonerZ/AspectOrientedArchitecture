using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core.Utilities.DependencyServiceTool;
using Application.Core.Utilities.Result;
using Application.Packages.AOP.Interceptor;
using Application.Packages.JWT.Service;
using Castle.DynamicProxy;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Packages.AOP.Aspects.Secure;

public class SecuredOperationAspect<T> : MethodInterceptor
    where T : class
{
    private string[] _roles;
    private IHttpContextAccessor _httpContextAccessor;
    private ITokenService _tokenService;

    public SecuredOperationAspect(string claims)
    {
        _roles = claims.Split(',');
        _tokenService =  DependencyServiceTool.ServiceProvider.GetService<ITokenService>();
        _httpContextAccessor =  DependencyServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
    }

    public override void Intercept(IInvocation invocation)
    {
        var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
        var tokenWithoutBearerKeyword = authorizationHeader.ToString().Split(' ')[1];
        var token = _tokenService.ResolveToken(tokenWithoutBearerKeyword);
        var roles = token.Claims;
        
        foreach (var role in roles)
        {
            if (_roles.Contains(role.Value))
            {
                invocation.Proceed();
                return;
            }
        }
        
        var methdReturnType = invocation.MethodInvocationTarget.ReturnType;

        if (methdReturnType == typeof(IResult)) invocation.ReturnValue = new ErrorResult("Unauthorized", "Access Denied!");

        else if (methdReturnType == typeof(IDataResult<T>)) invocation.ReturnValue = new ErrorDataResult<T>("Unauthorized", "Access Denied!");

        else throw new System.Exception("Acces Denied!");
    }
}