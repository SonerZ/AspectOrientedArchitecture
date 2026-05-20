using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Core.Entities.Concrete;
using Application.DataAccess.Abstract.Repository;
using Application.DataAccess.Entities;

namespace Application.DataAccess.Abstract;

public interface IActivityDal: IRepository<Activity, Guid>
{
    Task<IEnumerable<Activity>> GetActivitiesFullInclude();
}