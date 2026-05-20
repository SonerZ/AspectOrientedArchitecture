using System;
using System.Collections.Generic;
using System.Linq;
using Application.Core.Configuration.Context;
using Application.Core.Entities.Concrete;
using Application.DataAccess.Abstract;
using Application.DataAccess.Concrete.EntityFramework.Context;
using Application.DataAccess.Concrete.EntityFramework.Repository;

namespace Application.DataAccess.Concrete.EntityFramework;

public class UserDal : EfRepositoryBase<User, Guid>, IUserDal
{
    private readonly IApplicationConfigurationContext _configurationContext;

    public UserDal(IApplicationConfigurationContext context) : base(context.ConnectionString)
    {
        _configurationContext = context;
    }

    public User GetUser(string username, string password)
    {
        using (var context = new ApplicationDbContext(_configurationContext.ConnectionString))
        {
            return context.Users.FirstOrDefault(user => user.Username == username && user.PasswordHash == password);
        }
    }
}