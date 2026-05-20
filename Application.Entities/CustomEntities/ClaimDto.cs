using System;

namespace Application.Entities.CustomEntities.User;

public class ClaimDto
{
    public Guid Id { get; set; }
    public string Type { get; set; }
    public string Value { get; set; }
}