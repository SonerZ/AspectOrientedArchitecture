using System;

namespace Application.Entities.CustomEntities.User;

public class UserDto : LoginnedUser
{
    public List<UserRoleDto> Roles { get; set; }
    public TokenInformation TokenInformation { get; set; } = new();
}

public class TokenInformation
{
    public DateTime ExpiryDate { get; set; }
    public string Token { get; set; }
}