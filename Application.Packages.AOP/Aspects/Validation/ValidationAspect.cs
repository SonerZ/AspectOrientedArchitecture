using System;
using System.Collections.Generic;
using System.Linq;
using Application.Core.Utilities.Result;
using Application.Packages.AOP.Interceptor;
using Castle.DynamicProxy;
using FluentValidation;

namespace Application.Packages.AOP.Aspects.Validation;

public class ValidationAspect<T> : MethodInterceptor
    where T : class
{
    private readonly Type[] _validatorTypes;
    public ValidationAspect(params Type[] validatorTypes)
    {
        foreach (var validatorType in validatorTypes)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("Wrong Validation Type");
            }
        }
      
        _validatorTypes = validatorTypes;
    }

    public override async void Intercept(IInvocation invocation)
    {
        var parametersIsValidets = true;
        
        foreach (var validatorType in _validatorTypes)
        {
            var validator = (IValidator)Activator.CreateInstance(validatorType);
            var entityType = validatorType.BaseType.GetGenericArguments()[0];
            var entities = invocation.Arguments.Where(i => i.GetType() == entityType);

            foreach (var entity in entities)
            {
                var validationResult = await ValdationTool.Validate(validator, entity);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors
                        .Select(i => new KeyValuePair<string, string>(i.ErrorCode, i.ErrorMessage))
                        .ToList();

                    var methdReturnType = invocation.MethodInvocationTarget.ReturnType;

                    if (methdReturnType == typeof(IResult)) invocation.ReturnValue = new ErrorResult(errors);

                    else if (methdReturnType == typeof(IDataResult<T>)) invocation.ReturnValue = new ErrorDataResult<T>(errors);

                    else throw new ValidationException(validationResult.Errors);

                    parametersIsValidets = false;
                    break;
                }
            }
        }

        if (parametersIsValidets) invocation.Proceed();
    }
}