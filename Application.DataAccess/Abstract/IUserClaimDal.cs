using System;
using System.Collections.Generic;
using Application.Core.Entities.Concrete;
using Application.DataAccess.Abstract.Repository;
using Application.DataAccess.Entities;

namespace Application.DataAccess.Abstract;

public interface IUserClaimDal: IRepository<UserClaim, Guid>
{
   public List<UserClaim> GetClaims(Guid userId);
}