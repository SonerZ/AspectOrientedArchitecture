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

public class UserClaimDal : EfRepositoryBase<UserClaim, Guid>, IUserClaimDal
{
    private readonly IApplicationConfigurationContext _configurationContext;

    public UserClaimDal(IApplicationConfigurationContext context) : base(context.ConnectionString)
    {
        _configurationContext = context;
    }
   
    public List<UserClaim> GetClaims(Guid userId)
    {
        using (var context = new ApplicationDbContext(_configurationContext.ConnectionString))
        {
            return context.UserClaims.Where(user => user.UserId == userId)
                .Include(i=> i.Claim)
                .ToList();
        }
    }
}