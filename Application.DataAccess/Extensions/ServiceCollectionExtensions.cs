using Application.Core.Configuration.Context;
using Application.Core.Entities.Concrete;
using Application.DataAccess.Abstract;
using Application.DataAccess.Concrete.EntityFramework;
using Application.DataAccess.Concrete.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DataAccess.Extensions
{
    /// <summary>
    /// ServiceCollectionExtensions contains extended IServiceCollection's methods.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Register business module dependencies to IServiceCollection
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static void AddDataAccessModule(this IServiceCollection services, IApplicationConfigurationContext configurationContext)
        {
            services.AddDbContext<ApplicationDbContext>(opt=> opt.UseMySql(configurationContext.ConnectionString,ServerVersion.AutoDetect(configurationContext.ConnectionString)) ,ServiceLifetime.Singleton);
            
            services.AddDal();
        }

        public static void AddDal(this IServiceCollection services)
        {
            services.AddSingleton<IUserDal,UserDal>();
            services.AddSingleton<IUserRoleDal,UserRoleDal>();
            services.AddSingleton<IActivityDal,ActivityDal>();
            services.AddSingleton<IRoleDal,RoleDal>();
            services.AddSingleton<IFunctionDal,FunctionDal>();
            services.AddSingleton<IDepartmentDal,DepartmentDal>();
            services.AddSingleton<IClaimDal,ClaimDal>();
            services.AddSingleton<IUserClaimDal,UserClaimDal>();
            services.AddSingleton<IRoleClaimDal,RoleClaimDal>();
        }
    }
}
