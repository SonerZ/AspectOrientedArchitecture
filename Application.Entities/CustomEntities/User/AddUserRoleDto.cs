namespace Application.Entities.CustomEntities.User;

public class AddUserRoleDto
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}