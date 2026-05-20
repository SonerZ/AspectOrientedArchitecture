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

public class FunctionDal : EfRepositoryBase<Function, Guid>, IFunctionDal
{
    private readonly IApplicationConfigurationContext _configurationContext;

    public FunctionDal(IApplicationConfigurationContext context) : base(context.ConnectionString)
    {
        _configurationContext = context;
    }

    public IEnumerable<Function> GetFunctions()
    {
        using (var context = new ApplicationDbContext(_configurationContext.ConnectionString))
        {
            return context.Functions
               .ToList();
        }
    }
}