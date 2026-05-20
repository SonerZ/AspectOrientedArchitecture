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

public class RoleClaimDal : EfRepositoryBase<RoleClaim, Guid>, IRoleClaimDal
{
    private readonly IApplicationConfigurationContext _configurationContext;

    public RoleClaimDal(IApplicationConfigurationContext context) : base(context.ConnectionString)
    {
        _configurationContext = context;
    }
   
}