using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core.Configuration.Context;
using Application.Core.Entities.Concrete;
using Application.DataAccess.Abstract;
using Application.DataAccess.Concrete.EntityFramework.Context;
using Application.DataAccess.Concrete.EntityFramework.Repository;
using Microsoft.EntityFrameworkCore;

namespace Application.DataAccess.Concrete.EntityFramework;

public class ActivityDal : EfRepositoryBase<Activity, Guid>, IActivityDal
{
    private readonly IApplicationConfigurationContext _configurationContext;

    public ActivityDal(IApplicationConfigurationContext context) : base(context.ConnectionString)
    {
        _configurationContext = context;
    }
    
    public async Task<IEnumerable<Activity>> GetActivitiesFullInclude()
    {
        using (var context = new ApplicationDbContext(_configurationContext.ConnectionString))
        {
            return await context.Activities
                .Include(i => i.User)
                .ToListAsync();
        }
    }
}