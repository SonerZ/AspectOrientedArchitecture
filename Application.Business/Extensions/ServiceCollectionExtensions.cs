using Application.Business.Concrete;
using Application.Core.Configuration.Context;
using Application.DataAccess.Abstract.Profile;
using Application.DataAccess.Extensions;
using Application.Packages.AOP.InterceptModule;
using Application.Packages.Hashing.Core.Service;
using Application.Packages.Hashing.MD5.Service;
using Application.Packages.JWT.Service;
using Autofac;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Business.Extensions
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
        public static IServiceCollection AddBusinessModule(this IServiceCollection services, IApplicationConfigurationContext configurationContext)
        {
            services.AddValidatorsFromAssemblyContaining(typeof(ServiceCollectionExtensions));

            services.AddAutoMapper(typeof(ProfileBase));

            services.AddDataAccessModule(configurationContext);

            services.AddSingleton<IUserManager, UserManager>();
            services.AddSingleton<IActivityManager, ActivityManager>();
            services.AddSingleton<IHashService, MD5HashService>();
            services.AddSingleton<ITokenService, JWTTokenService>();
            services.AddSingleton<IDepartmentManager, DepartmentManager>();
         
            return services;
        }
        public static void AddBusinessModule(this ContainerBuilder builder)
        {
            var interceptorModule = new AutofacInterceptorModule();
            interceptorModule.Load(typeof(ServiceCollectionExtensions).Assembly);
            builder.RegisterModule(interceptorModule);
        }
    }
}
