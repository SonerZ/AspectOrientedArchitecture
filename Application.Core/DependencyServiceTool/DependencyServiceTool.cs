using System;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Core.Utilities.DependencyServiceTool;

public static class DependencyServiceTool
{
    public  static  IServiceProvider  ServiceProvider { get; set; }
    public static void CreateServiceProvider(IServiceCollection services)
    {
        ServiceProvider = services.BuildServiceProvider();
    }
}