

using Application.Core.Entities.Concrete;
using Application.DataAccess.Entities;
using Application.Entities.CustomEntities;
using Application.Entities.CustomEntities.User;

namespace Application.DataAccess.Abstract.Profile;

public class ProfileBase : AutoMapper.Profile
{
    public ProfileBase()
    {
        CreateMap<ActivityDto, Activity>().ReverseMap();
        CreateMap<ActivityCreateDto, Activity>().ReverseMap();
        CreateMap<ActivityBaseDto, Activity>().ReverseMap();
        
        CreateMap<UserCreateDto, User>().ReverseMap();
        CreateMap<RoleDto, Role>().ReverseMap();
        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<DepartmentDto, Department>().ReverseMap();
        CreateMap<CreateDepartmentDto, Department>().ReverseMap();
        CreateMap<FunctionDto, Function>().ReverseMap();
        CreateMap<CreateFunctionDto, Function>().ReverseMap();
        CreateMap<LoginnedUser, User>().ReverseMap();
        CreateMap<UserRoleDto, UserRole>().ReverseMap();
        CreateMap<AddUserRoleDto, UserRole>().ReverseMap();
        CreateMap<RemoveUserRoleDto, UserRole>().ReverseMap();
        CreateMap<CreateRoleDto, Role>().ReverseMap();
        CreateMap<UpdateFunctionDto, Function>().ReverseMap();
        CreateMap<UpdateDepartmentDto, Department>().ReverseMap();
        CreateMap<UpdateRoleDto, Role>().ReverseMap();
        CreateMap<ClaimDto, Claim>().ReverseMap();
        CreateMap<UpdateClaimDto, Claim>().ReverseMap();
        CreateMap<CreateClaimDto, Claim>().ReverseMap();
        CreateMap<CreateUserClaimDto, UserClaim>().ReverseMap();
        CreateMap<CreateRoleClaimDto, RoleClaim>().ReverseMap();
    }
}