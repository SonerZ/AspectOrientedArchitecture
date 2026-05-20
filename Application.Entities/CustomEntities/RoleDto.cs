using System;

namespace Application.Entities.CustomEntities.User;

public class RoleDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Title { get; set; }
}