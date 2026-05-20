using System;
using System.Collections.Generic;
using System.Linq;
using Application.Core.Configuration.Context;
using Application.Core.Entities.Concrete;
using Application.DataAccess.Abstract;
using Application.DataAccess.Concrete.EntityFramework.Context;
using Application.DataAccess.Concrete.EntityFramework.Repository;
using Microsoft.EntityFrameworkCore;

namespace Application.DataAccess.Concrete.EntityFramework;

public class RoleDal : EfRepositoryBase<Role, Guid>, IRoleDal
{
    private readonly IApplicationConfigurationContext _configurationContext;

    public RoleDal(IApplicationConfigurationContext context) : base(context.ConnectionString)
    {
        _configurationContext = context;
    }

    public IEnumerable<Role> GetRoles()
    {
        using (var context = new ApplicationDbContext(_configurationContext.ConnectionString))
        {
            return context.Roles
               .ToList();
        }
    }
}