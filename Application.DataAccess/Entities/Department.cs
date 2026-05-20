using System;
using Application.DataAccess.Entities.Concrete;

namespace Application.DataAccess.Entities;

public class Department : BaseEntity<Guid>
{
    public string Name { get; set; }
    public Function Function { get; set; }
    public Guid FunctionId { get; set; }
}