using System;
using System.Collections.Generic;
using System.Linq;
using Application.Core.Configuration.Context;
using Application.Core.Entities.Concrete;
using Application.DataAccess.Abstract;
using Application.DataAccess.Concrete.EntityFramework.Context;
using Application.DataAccess.Concrete.EntityFramework.Repository;
using Application.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.DataAccess.Concrete.EntityFramework;

public class DepartmentDal : EfRepositoryBase<Department, Guid>, IDepartmentDal
{
    private readonly IApplicationConfigurationContext _configurationContext;

    public DepartmentDal(IApplicationConfigurationContext context) : base(context.ConnectionString)
    {
        _configurationContext = context;
    }

    public IEnumerable<Department> GetDepartments()
    {
        using (var context = new ApplicationDbContext(_configurationContext.ConnectionString))
        {
            return context.Departments
                .Include(i=> i.Function)
               .ToList();
        }
    }
}