using System;
using System.Collections.Generic;
using Application.Core.Entities.Concrete;
using Application.DataAccess.Abstract.Repository;
using Application.DataAccess.Entities;

namespace Application.DataAccess.Abstract;

public interface IRoleClaimDal: IRepository<RoleClaim, Guid>
{
}