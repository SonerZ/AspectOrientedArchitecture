using System;
using System.Collections.Generic;
using Application.Core.Entities.Concrete;
using Application.DataAccess.Abstract.Repository;
using Application.DataAccess.Entities;

namespace Application.DataAccess.Abstract;

public interface IDepartmentDal: IRepository<Department, Guid>
{
    IEnumerable<Department> GetDepartments();
}