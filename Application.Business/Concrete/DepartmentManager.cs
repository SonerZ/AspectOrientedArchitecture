using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Core.Constants.Messages;
using Application.Core.Entities.Concrete;
using Application.Core.Utilities.Result;
using Application.DataAccess.Abstract;
using Application.DataAccess.Entities;
using Application.Entities.CustomEntities;
using Application.Entities.CustomEntities.User;
using Application.Packages.Hashing.Core.Service;
using Application.Packages.JWT.Service;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using IResult = Application.Core.Utilities.Result.IResult;

namespace Application.Business.Concrete;

public class DepartmentManager : IDepartmentManager
{
    private readonly IMapper _mapper;
    private readonly IUserDal _userDal;
    private readonly IHashService _hashService;
    private readonly ITokenService _tokenService;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IUserRoleDal _userRoleDal;
    private readonly IDepartmentDal _departmentDal;
    private readonly IFunctionDal _functionDal;

    public DepartmentManager(IHttpContextAccessor httpContextAccessor,IMapper mapper, IUserDal userDal, IHashService hashService
    ,IUserRoleDal userRoleDal, ITokenService tokenService, IDepartmentDal departmentDal, IFunctionDal functionDal)
    {
        _mapper = mapper;
        _userDal = userDal;
        _hashService = hashService;
        _tokenService = tokenService;
        _userRoleDal = userRoleDal;
        _contextAccessor = httpContextAccessor;
        _departmentDal = departmentDal;
        _functionDal = functionDal;
    }

    public IResult CreateDepartment(CreateDepartmentDto model)
    {
        var entity = _mapper.Map<Department>(model);

        var findUser = _departmentDal.Find(i => i.Name == entity.Name);

        if (findUser != null)
            return new ErrorResult(ResultMessages.NotBeAdded);

        var isAdded = _departmentDal.Add(entity,string.Empty);

        if(!isAdded)
            return new ErrorResult(ResultMessages.NotBeAdded);

        return new SuccessResult();
    }

    public IResult CreateFunction(CreateFunctionDto model)
    {
        var entity = _mapper.Map<Function>(model);

        var findUser = _functionDal.Find(i => i.Name == entity.Name);

        if (findUser != null)
            return new ErrorResult(ResultMessages.NotBeAdded);

        var isAdded = _functionDal.Add(entity,string.Empty);

        if(!isAdded)
            return new ErrorResult(ResultMessages.NotBeAdded);

        return new SuccessResult();
    }
    
    public IResult UpdateFunction(UpdateFunctionDto model)
    {
        var entity = _mapper.Map<Function>(model);

        var findUser = _functionDal.Find(i => i.Id == entity.Id);

        if (findUser == null)
            return new ErrorResult(ResultMessages.NotBeAdded);


        findUser.Name = model.Name;
        
        var isAdded = _functionDal.Update(entity,string.Empty);

        if(!isAdded)
            return new ErrorResult(ResultMessages.NotBeAdded);

        return new SuccessResult();
    }

    public IResult UpdateDepartment(UpdateDepartmentDto model)
    {
        var entity = _mapper.Map<Department>(model);

        var findUser = _departmentDal.Find(i => i.Id == entity.Id);

        if (findUser == null)
            return new ErrorResult(ResultMessages.NotBeAdded);


        findUser.Name = model.Name;
        
        var isAdded = _departmentDal.Update(findUser,string.Empty);

        if(!isAdded)
            return new ErrorResult(ResultMessages.NotBeAdded);

        return new SuccessResult();
    }

    public IResult RemoveFunction(Guid id)
    {
        var findUser = _functionDal.Find(i => i.Id == id);

        if (findUser == null)
            return new ErrorResult(ResultMessages.NotBeAdded);

        var isAdded = _functionDal.Remove(findUser);

        if(!isAdded)
            return new ErrorResult(ResultMessages.NotBeAdded);

        return new SuccessResult();
    }
    
    public IResult RemoveDepartment(Guid id)
    {
        var findUser = _departmentDal.Find(i => i.Id == id);

        if (findUser == null)
            return new ErrorResult(ResultMessages.NotBeAdded);

        var isAdded = _departmentDal.Remove(findUser);

        if(!isAdded)
            return new ErrorResult(ResultMessages.NotBeAdded);

        return new SuccessResult();
    }

    public IDataResult<DepartmentDto> GetDepartment(string id)
    {
        var department = _departmentDal.Find(i=> i.Id == Guid.Parse(id));

        if(department is null)
            return new ErrorDataResult<DepartmentDto>(ResultMessages.NotFound);

        var departmentDto = _mapper.Map<DepartmentDto>(department);

        return new SuccessDataResult<DepartmentDto>(departmentDto);
    }

    public IDataResult<List<DepartmentDto>> GetDepartments(Guid? function)
    {
        var roles = _departmentDal.GetDepartments();

        if (function != null)
        {
            roles = roles.Where(i=> i.FunctionId==function);
        }

        if(roles is null)
            return new ErrorDataResult<List<DepartmentDto>>(ResultMessages.NotFound);

        var userResp = _mapper.Map<List<DepartmentDto>>(roles);

        return new SuccessDataResult<List<DepartmentDto>>(userResp);
    }

    public IDataResult<List<FunctionDto>> GetFunctions()
    {
        var functions = _functionDal.GetList();

        if(functions is null)
            return new ErrorDataResult<List<FunctionDto>>(ResultMessages.NotFound);

        var userResp = _mapper.Map<List<FunctionDto>>(functions);

        return new SuccessDataResult<List<FunctionDto>>(userResp);
    }
}