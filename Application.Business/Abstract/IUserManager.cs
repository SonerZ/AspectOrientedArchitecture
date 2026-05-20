using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Business.ValidationRules.FluentValidation;
using Application.Core.Entities.Concrete;
using Application.Core.Utilities.Result;
using Application.Entities.CustomEntities;
using Application.Entities.CustomEntities.User;
using Application.Packages.AOP.Aspects.Exception;
using Application.Packages.AOP.Aspects.Performance;
using Application.Packages.AOP.Aspects.Transaction;
using Application.Packages.AOP.Aspects.Validation;

public interface IUserManager
{
    /// <summary>
    /// Add new user
    /// </summary>
    /// <param name="user">user data</param>
    /// <returns></returns>
    //[UnitOfWorkAspect]
    [ExceptionAspect]
    [ValidationAspect<IResult>(typeof(UserValidator))]
    IResult Create(UserCreateDto user);
    
    IResult Remove(Guid key);
    /// <summary>
    /// Get loginned user data
    /// </summary>
    /// <returns></returns>
    [ExceptionAspect]
    Task<IDataResult<LoginnedUser>> AuthenticatedUser();

    [ExceptionAspect]
    IDataResult<LoginnedUser> GetUser(string username);
    
    [ExceptionAspect]
    IDataResult<List<UserDto>> GetUsers();
    
    [ExceptionAspect]
    IDataResult<List<RoleDto>> GetRoles();
    
    [ExceptionAspect(Priority = 1)]
    [PerformanceAspect(1000, Priority = 2)]

    [ValidationAspect<IDataResult<UserDto>>(typeof(UserLoginValidator))]
    IDataResult<UserDto> Login(UserLoginDto request);

    IResult RemoveUserRole(RemoveUserRoleDto model);
    IResult AddUserRole(AddUserRoleDto model);
    IDataResult<List<UserRoleDto>> UserRoles(Guid userId);

    IResult RemoveRole(Guid roleId);
    IResult AddRole(CreateRoleDto model);
    IResult UpdateRole(UpdateRoleDto model);

    IResult RemoveClaim(Guid id);

    IDataResult<List<ClaimDto>> GetClaims();

    IResult AddClaim(CreateClaimDto model);
    
    IResult AddUserClaim(CreateUserClaimDto model);
    
    IResult AddRoleClaim(CreateRoleClaimDto model);
    IResult RemoveRoleClaim(CreateRoleClaimDto model);
    IResult RemoveUserClaim(CreateUserClaimDto model);
    
    IResult UpdateClaim(UpdateClaimDto model);
}