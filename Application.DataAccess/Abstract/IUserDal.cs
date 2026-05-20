using System;
using System.Collections.Generic;
using Application.Core.Entities.Concrete;
using Application.DataAccess.Abstract.Repository;
using Application.DataAccess.Entities;

namespace Application.DataAccess.Abstract;

public interface IUserDal: IRepository<User, Guid>
{
    User GetUser(string username, string password);
}