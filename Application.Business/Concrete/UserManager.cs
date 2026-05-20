using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Core.Constants.Messages;
using Application.Core.Entities.Concrete;
using Application.Core.Utilities.Result;
using Application.DataAccess.Abstract;
using Application.Entities.CustomEntities;
using Application.Entities.CustomEntities.User;
using Application.Packages.Hashing.Core.Service;
using Application.Packages.JWT.Service;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Claim = System.Security.Claims.Claim;
using IResult = Application.Core.Utilities.Result.IResult;

namespace Application.Business.Concrete;

public class UserManager : IUserManager
{
    private readonly IMapper _mapper;
    private readonly IUserDal _userDal;
    private readonly IHashService _hashService;
    private readonly ITokenService _tokenService;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IUserRoleDal _userRoleDal;
    private readonly IRoleDal _roleDal;
    private readonly IClaimDal _claimDal;
    private readonly IUserClaimDal _userClaimDal;
    private readonly IRoleClaimDal _roleClaimDal;

    public UserManager(IHttpContextAccessor httpContextAccessor,IMapper mapper, IUserDal userDal, IHashService hashService
    ,IUserRoleDal userRoleDal, ITokenService tokenService, IRoleDal roleDal, IClaimDal claimDal, IUserClaimDal userClaimDal, IRoleClaimDal roleClaimDal)
    {
        _mapper = mapper;
        _userDal = userDal;
        _hashService = hashService;
        _tokenService = tokenService;
        _userRoleDal = userRoleDal;
        _contextAccessor = httpContextAccessor;
        _roleDal = roleDal;
        _claimDal = claimDal;
        _userClaimDal = userClaimDal;
        _roleClaimDal = roleClaimDal;
    }
   
    public IResult Create(UserCreateDto user)
    {
        if (user.Password != user.ConfirmPassword)
        {
            return new ErrorResult("password and confirmation password do not match");
        }
        var entity = _mapper.Map<User>(user);

        var findUser = _userDal.Find(i => i.Username == entity.Username);

        if (findUser != null)
            return new ErrorResult(ResultMessages.NotBeAdded);

        entity.PasswordHash = _hashService.Generate(user.Password);

        var isAdded = _userDal.Add(entity, user.Username);

        if(!isAdded)
            return new ErrorResult(ResultMessages.NotBeAdded);

        return new SuccessResult();
    }
    
    public IResult Remove(Guid key)
    {
        var findUser = _userDal.Find(i => i.Id == key);

        if (findUser == null)
            return new ErrorResult(ResultMessages.NotBeAdded);

        var isDeleted = _userDal.Remove(findUser);

        if(!isDeleted)
            return new ErrorResult(ResultMessages.NotBeRemoved);

        return new SuccessResult();
    }
    
    public IResult AddUserRole(AddUserRoleDto model)
    {
        var entity = _mapper.Map<UserRole>(model);

        var findUser = _userRoleDal.Find(i => i.UserId == entity.UserId && i.RoleId == model.RoleId);

        if (findUser != null)
            return new ErrorResult(ResultMessages.NotBeAdded);

        var isAdded = _userRoleDal.Add(entity,string.Empty);

        if(!isAdded)
            return new ErrorResult(ResultMessages.NotBeAdded);

        return new SuccessResult();
    }
    
    public IResult RemoveUserRole(RemoveUserRoleDto model)
    {
        var entity = _mapper.Map<UserRole>(model);

        var findUser = _userRoleDal.Find(i => i.UserId == entity.UserId && i.RoleId == model.RoleId);

        if (findUser == null)
            return new ErrorResult(ResultMessages.NotBeAdded);

        var isAdded = _userRoleDal.Remove(entity);

        if(!isAdded)
            return new ErrorResult(ResultMessages.NotBeAdded);

        return new SuccessResult();
    }


    public async Task<IDataResult<LoginnedUser>> AuthenticatedUser()
    {
        var token = _contextAccessor.HttpContext.Request.Headers["Authorization"];
        var tokenWithoutBearerKeyword = token.ToString().Split(' ')[1];

        var resolveTokenResult = _tokenService.ResolveToken(tokenWithoutBearerKeyword);

        if (!resolveTokenResult.IsValid)
            return new ErrorDataResult<LoginnedUser>(resolveTokenResult.ErrorMessage);

        var loginnedUsernameData = resolveTokenResult.Claims
            .FirstOrDefault(c => c.Type == "unique_name");
           
        var currentUser = _userDal
            .Find(x =>  x.DeletedDate == null && x.Username == loginnedUsernameData.Value);

        if (currentUser is null)
            return new ErrorDataResult<LoginnedUser>(ResultMessages.NotFound);

        var userDto = _mapper.Map<LoginnedUser>(currentUser);

        return new SuccessDataResult<LoginnedUser>(userDto);
    }

    public IDataResult<LoginnedUser> GetUser(string username)
    {
       var user = _userDal.Find(i=> i.Username == username);

       if(user is null)
            return new ErrorDataResult<LoginnedUser>(ResultMessages.NotFound);

        var userResp = _mapper.Map<UserDto>(user);

        return new SuccessDataResult<LoginnedUser>(userResp);
    }
    
    public IDataResult<List<UserDto>> GetUsers()
    {
        var users = _userDal.GetList();
     
        if(users is null)
            return new ErrorDataResult<List<UserDto>>(ResultMessages.NotFound);

        var userResp = _mapper.Map<List<UserDto>>(users);
        
        foreach (var user in userResp)
        {
            var roles = _userRoleDal.FindAll(i => i.UserId == user.Id);
            var userRoles = _mapper.Map<List<UserRoleDto>>(roles);
            
            foreach (var role in userRoles)
            {
                var roleResp = _roleDal.Find(i=> i.Id == role.Role.Id);
                var resp = _mapper.Map<RoleDto>(roleResp);
                role.Role = resp;
            }

            user.Roles = userRoles;
        }


        return new SuccessDataResult<List<UserDto>>(userResp);
    }
    
    public IDataResult<List<RoleDto>> GetRoles()
    {
        var roles = _roleDal.GetList();

        if(roles is null)
            return new ErrorDataResult<List<RoleDto>>(ResultMessages.NotFound);

        var userResp = _mapper.Map<List<RoleDto>>(roles);

        return new SuccessDataResult<List<RoleDto>>(userResp);
    }
    
    public IDataResult<List<ClaimDto>> GetClaims()
    {
        var claims = _claimDal.GetList();

        if(claims is null)
            return new ErrorDataResult<List<ClaimDto>>(ResultMessages.NotFound);

        var userResp = _mapper.Map<List<ClaimDto>>(claims);

        return new SuccessDataResult<List<ClaimDto>>(userResp);
    }
    
    public IResult AddRole(CreateRoleDto model)
    {
        var userResp = _mapper.Map<Role>(model);

        var role = _roleDal.Find(i => i.Name == model.Name);

        if (role != null)
        {
            return new ErrorResult();
        }
        
        _roleDal.Add(userResp, string.Empty);

        return new SuccessResult();
    }
    
    public IResult AddClaim(CreateClaimDto model)
    {
        var userResp = _mapper.Map<Application.Core.Entities.Concrete.Claim>(model);

        var role = _claimDal.Find(i => i.Type == model.Type);

        if (role != null)
        {
            return new ErrorResult();
        }
        
        _claimDal.Add(userResp, string.Empty);

        return new SuccessResult();
    }
    
    public IResult AddUserClaim(CreateUserClaimDto model)
    {
        var userResp = _mapper.Map<UserClaim>(model);

        var role = _userClaimDal.Find(i => i.UserId == model.UserId && i.ClaimId == model.ClaimId);

        if (role != null)
        {
            return new SuccessResult();
        }
        
        _userClaimDal.Add(userResp, string.Empty);

        return new SuccessResult();
    }
    
    public IResult RemoveUserClaim(CreateUserClaimDto model)
    {
        var role = _userClaimDal.Find(i => i.UserId == model.UserId && i.ClaimId == model.ClaimId);

        if (role == null)
        {
            return new ErrorResult();
        }
        
        _userClaimDal.Remove(role);

        return new SuccessResult();
    }
    
    public IResult RemoveRoleClaim(CreateRoleClaimDto model)
    {
        var role = _roleClaimDal.Find(i => i.RoleId == model.RoleId && i.ClaimId == model.ClaimId);

        if (role == null)
        {
            return new ErrorResult();
        }
        
        _roleClaimDal.Remove(role);

        return new SuccessResult();
    }
    
    public IResult AddRoleClaim(CreateRoleClaimDto model)
    {
        var userResp = _mapper.Map<RoleClaim>(model);

        var role = _roleClaimDal.Find(i => i.RoleId == model.RoleId && i.ClaimId == model.ClaimId);

        if (role != null)
        {
            return new SuccessResult();
        }
        
        _roleClaimDal.Add(userResp, string.Empty);

        return new SuccessResult();
    }
    
    public IResult UpdateClaim(UpdateClaimDto model)
    {
        var claim = _claimDal.Find(i => i.Id == model.Id);

        if (claim is null)
        {
            return new ErrorResult();
        }

        claim.Type = model.Type;
        claim.Value = model.Value;
        
        _claimDal.Update(claim, string.Empty);

        return new SuccessResult();
    }
    
    
    public IResult RemoveClaim(Guid id)
    {
        var role = _claimDal.Find(i => i.Id == id);

        if (role == null)
        {
            return new ErrorResult();
        }
        
        _claimDal.Remove(role);

        return new SuccessResult();
    }
    
    public IResult UpdateRole(UpdateRoleDto model)
    {
        var userResp = _mapper.Map<Role>(model);

        var role = _roleDal.Find(i => i.Id == model.Id);

        if (role == null)
        {
            return new ErrorResult();
        }
        
        role.Name = model.Name;
        role.Title = model.Title;
        
        _roleDal.Update(role, string.Empty);

        return new SuccessResult();
    }
    
    public IResult RemoveRole(Guid roleId)
    {
        var role = _roleDal.Find(i => i.Id == roleId);

        if (role == null)
        {
            return new ErrorResult();
        }
        
        _roleDal.Remove(role);

        return new SuccessResult();
    }
    
    public IDataResult<List<UserRoleDto>> UserRoles(Guid userId)
    {
        var roles = _userRoleDal.GetList();

        if(roles is null)
            return new ErrorDataResult<List<UserRoleDto>>(ResultMessages.NotFound);
        
        roles = roles.Where(i => i.UserId == userId)
            .ToList();

        var userResp = _mapper.Map<List<UserRoleDto>>(roles);

        return new SuccessDataResult<List<UserRoleDto>>(userResp);
    }

    private IEnumerable<Claim> GenerateUserClaims(User user)
    {
        var roles = _userRoleDal.GetUserRoles(user.Id);

        var claimsUser = _userClaimDal.GetClaims(user.Id);

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
        };

        foreach (var role in roles)
        {
            var claimsRole = _roleClaimDal.FindAll(i => i.RoleId == role.RoleId).ToList();
            
            claimsRole.ForEach(rl =>
            {
                var entity = _claimDal.Find(i => i.Id == rl.ClaimId);
                
                claims.Add(new Claim(ClaimTypes.Role, entity.Value.ToString()));
            });
        }
        
        foreach (var userClaim in claimsUser)
            claims.Add(new Claim(ClaimTypes.Role, userClaim.Claim.Value.ToString()));
        
        return claims;
    }
    public IDataResult<UserDto> Login(UserLoginDto loginRequest)
    {
        var userLoginResponse = new UserDto();

        var hashedPassword = _hashService.Generate(loginRequest.Password);

        var user = _userDal.GetUser(loginRequest.Username, hashedPassword);

        if (user == null)
            return new ErrorDataResult<UserDto>( ResultMessages.Incorrect);

        var claims = GenerateUserClaims(user);
        
        var tokenResult = _tokenService.Generate(claims);

        user.LastLoginDate= DateTime.Now;

        _userDal.Update(user, user.Username);
        
        userLoginResponse = _mapper.Map<UserDto>(user);
        
        userLoginResponse.TokenInformation.ExpiryDate = tokenResult.ExpiryDate;
        userLoginResponse.TokenInformation.Token = tokenResult.Token;

        return new SuccessDataResult<UserDto>(userLoginResponse);
    }
}