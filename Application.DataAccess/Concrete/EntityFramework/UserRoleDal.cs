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

public class UserRoleDal : EfRepositoryBase<UserRole, Guid>, IUserRoleDal
{
    private readonly IApplicationConfigurationContext _configurationContext;

    public UserRoleDal(IApplicationConfigurationContext context) : base(context.ConnectionString)
    {
        _configurationContext = context;
    }

    public IEnumerable<UserRole> GetUserRoles(Guid userId)
    {
        using (var context = new ApplicationDbContext(_configurationContext.ConnectionString))
        {
            return context.UserRoles.Where(i=> i.UserId == userId)
                .Include(i=> i.User)
                .Include(i=> i.Role)
                .ToList();
        }
    }
}