using System;
using Application.DataAccess.Entities.Concrete;

namespace Application.DataAccess.Entities;

public class Function : BaseEntity<Guid>
{
    public string Name { get; set; }
}