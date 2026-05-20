using Application.Core.Entities.Concrete;
using Application.DataAccess.Abstract.Repository;
using Application.DataAccess.Concrete.EntityFramework.Context;
using Application.DataAccess.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.DataAccess.Concrete.EntityFramework.Repository
{
    /// <summary>
    /// Generic repository base.
    /// </summary>
    /// <typeparam name="TEntity">TEntity is database entity.</typeparam>
    /// <typeparam name="TKey">Unique key of TEntity.</typeparam>
    public class EfRepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
    {

        private readonly string _connectionString;

        public EfRepositoryBase(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool Add(TEntity entity, string userId)
        {
            using (var context = new ApplicationDbContext(_connectionString))
            {
                entity.CreatedDate = DateTime.Now;

                entity.CreatedUsername = userId;

                context.Attach<TEntity>(entity).State = EntityState.Added;
                return context.SaveChanges() > 0;
            }
        }

        public bool AddRange(List<TEntity> entities)
        {
            using (var context = new ApplicationDbContext(_connectionString))
            {
                context.AddRange(entities);
                return context.SaveChanges() > 0;
            }
        }

        public async Task<bool> AddAsync(TEntity entity, string userId)
        {
            using (var context = new ApplicationDbContext(_connectionString))
            {
                entity.CreatedDate = DateTime.UtcNow;
                entity.ModifiedDate = DateTime.UtcNow;
             
                entity.CreatedUsername = userId;
              
                context.Attach<TEntity>(entity).State = EntityState.Added;
                return await context.SaveChangesAsync() > 0;
            }
        }

        public TEntity GetById(TKey id)
        {
            using (var context = new ApplicationDbContext(_connectionString))
            {
                return Queryable.FirstOrDefault<TEntity>(context.Set<TEntity>()
                        .Where(i=> !i.DeletedDate.HasValue),
                    (Expression<Func<TEntity, bool>>)(entity => (bool)entity.Id.Equals((object)id)));
            }
        }

        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            using (var context = new ApplicationDbContext(_connectionString))
            {
                return await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync<TEntity>(context.Set<TEntity>()
                        .Where(i=> !i.DeletedDate.HasValue),
                    (Expression<Func<TEntity, bool>>)(entity => (bool)entity.Id.Equals((object)id)));
            }
        }

        public int GetCount()
        {
            using (var context = new ApplicationDbContext(_connectionString))
            {
                return Queryable.Count<TEntity>(context.Set<TEntity>().Where(i=> !i.DeletedDate.HasValue));
            }
        }

        public async Task<int> GetCountAsync()
        {
            using (var context = new ApplicationDbContext(_connectionString))
            {
                return await EntityFrameworkQueryableExtensions.CountAsync<TEntity>(context.Set<TEntity>().Where(i=> !i.DeletedDate.HasValue));
            }
        }

        public List<TEntity> GetList()
        {
            using (var context = new ApplicationDbContext(_connectionString))
            {
                return Enumerable.ToList<TEntity>(context.Set<TEntity>().Where(i=> !i.DeletedDate.HasValue).OrderByDescending(i => i.ModifiedDate));
            }
        }

        public async Task<List<TEntity>> GetListAsync()
        {
            using (var context = new ApplicationDbContext(_connectionString))
            {
                return await EntityFrameworkQueryableExtensions.ToListAsync(context.Set<TEntity>()
                    .OrderByDescending(i => i.ModifiedDate));
            }
        }

        public bool Remove(TEntity entity)
        {
            using (var context = new ApplicationDbContext(_connectionString))
            {
                context.Attach<TEntity>(entity).State = EntityState.Deleted;
                return context.SaveChanges() > 0;
            }
        }

        public bool SoftRemove(TKey id, string userId)
        {
            using (var context = new ApplicationDbContext(_connectionString))
            {
                var entity = GetById(id);

                entity.DeletedDate = DateTime.UtcNow;
                entity.DeletedUsername = userId;

                context.Attach<TEntity>(entity).State = EntityState.Modified;
                return context.SaveChanges() > 0;
            }
        }

        public async Task<bool> SoftRemoveAsync(TKey id, string userId)
        {
            using (var context = new ApplicationDbContext(_connectionString))
            {
                var entity = await GetByIdAsync(id);

                entity.DeletedDate = DateTime.UtcNow;
                entity.DeletedUsername = userId;

                context.Attach<TEntity>(entity).State = EntityState.Modified;
                return await context.SaveChangesAsync() > 0;
            }
        }
        public async Task<bool> RemoveAsync(TEntity entity)
        {
            using (var context = new ApplicationDbContext(_connectionString))
            {
                context.Attach<TEntity>(entity).State = EntityState.Deleted;
                return await context.SaveChangesAsync() > 0;
            }
        }

        public async Task<bool> RemoveRangeAsync(List<TEntity> entities)
        {
            using (var context = new ApplicationDbContext(_connectionString))
            {
                context.RemoveRange(entities);
                return await context.SaveChangesAsync() > 0;
            }
        }

        public async Task<bool> RemoveAsyncById(TKey id)
        {
            using (var context = new ApplicationDbContext(_connectionString))
            {
                TEntity entity = await GetByIdAsync(id);
                context.Attach<TEntity>(entity).State = EntityState.Deleted;
                return await context.SaveChangesAsync() > 0;
            }
        }

        public bool Update(TEntity entity, string userId)
        {
            using (var context = new ApplicationDbContext(_connectionString))
            {
                var existingEntity = context.Set<TEntity>().Find(entity.Id);
    
                if (existingEntity == null)
                {
                    throw new DbUpdateConcurrencyException("Güncellenecek veri bulunamadı veya daha önce silinmiş.");
                }

                entity.ModifiedDate = DateTime.UtcNow;
                entity.ModifiedUsername = userId;

                context.Entry(existingEntity).CurrentValues.SetValues(entity);
                context.Entry(existingEntity).State = EntityState.Modified;

                return context.SaveChanges() > 0;
            }

        }

        public async Task<bool> UpdateAsync(TEntity entity, string userId)
        {
            using (var context = new ApplicationDbContext(_connectionString))
            {
                entity.ModifiedDate = DateTime.UtcNow;
               
                entity.ModifiedUsername = userId;

                context.Attach<TEntity>(entity).State = EntityState.Modified;

                return await context.SaveChangesAsync() > 0;
            }
        }
        public TEntity Find(Expression<Func<TEntity, bool>> expression)
        {
            using (var context = new ApplicationDbContext(_connectionString))
            {
                using var cnt = new ApplicationDbContext(GetDbOption());
                return cnt.Set<TEntity>().Where(expression).Where(i=> !i.DeletedDate.HasValue).AsNoTracking().FirstOrDefault();
            }
        }
        public DbContextOptions<ApplicationDbContext> GetDbOption()
        {
            using (var context = new ApplicationDbContext(_connectionString))
            {
                var opt = new DbContextOptionsBuilder<ApplicationDbContext>();
                opt.UseMySql(context.Database.GetConnectionString(), ServerVersion.AutoDetect(context.Database.GetConnectionString()), options => options.EnableRetryOnFailure());
                opt.EnableDetailedErrors();
                opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                return opt.Options;
            }
        }
        public IList<TEntity> FindAll(Expression<Func<TEntity, bool>> expression)
        {
            using var cnt = new ApplicationDbContext(GetDbOption());
            return cnt.Set<TEntity>().Where(w => !w.DeletedDate.HasValue).Where(expression).AsNoTracking().ToList();
        }
    }
}
