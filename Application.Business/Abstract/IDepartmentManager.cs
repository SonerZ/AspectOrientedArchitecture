using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Business.ValidationRules.FluentValidation;
using Application.Core.Utilities.Result;
using Application.Entities.CustomEntities;
using Application.Entities.CustomEntities.User;
using Application.Packages.AOP.Aspects.Exception;
using Application.Packages.AOP.Aspects.Performance;
using Application.Packages.AOP.Aspects.Transaction;
using Application.Packages.AOP.Aspects.Validation;

public interface IDepartmentManager
{
    [ExceptionAspect]
    [ValidationAspect<IResult>(typeof(UserValidator))]
    IResult CreateDepartment(CreateDepartmentDto model);

    [ExceptionAspect]
    [ValidationAspect<IResult>(typeof(UserValidator))]
    IResult CreateFunction(CreateFunctionDto model);

    IResult RemoveFunction(Guid id);
    IResult UpdateFunction(UpdateFunctionDto model);  
    
    IResult UpdateDepartment(UpdateDepartmentDto model);

    IResult RemoveDepartment(Guid id);

    [ExceptionAspect]
    IDataResult<DepartmentDto> GetDepartment(string id);
    
    [ExceptionAspect]
    IDataResult<List<DepartmentDto>> GetDepartments(Guid? function);
    
    [ExceptionAspect]
    IDataResult<List<FunctionDto>> GetFunctions();
}