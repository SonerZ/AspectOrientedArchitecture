using System.Linq;
using Castle.DynamicProxy;
using Newtonsoft.Json;

namespace Application.Packages.AOP.Helpers;

public static class AspectHelper
{
    public static string GetMethodKey(IInvocation invocation)
    {
        var fullName = $"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}";

        var arguments = invocation.Arguments.ToArray();

        var key = $"{fullName}({JsonConvert.SerializeObject(arguments)})";

        return key;
    }
}