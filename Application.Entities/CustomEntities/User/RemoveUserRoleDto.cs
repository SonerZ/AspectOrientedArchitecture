namespace Application.Entities.CustomEntities.User;

public class RemoveUserRoleDto
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}