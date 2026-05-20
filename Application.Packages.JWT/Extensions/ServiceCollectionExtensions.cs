using System;
using System.Collections.Generic;
using System.Text;
using Application.Packages.JWT.Configuration;
using Application.Packages.JWT.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Packages.JWT.Extensions
{
    /// <summary>
    /// ServiceCollectionExtensions contains extended IServiceCollection's methods.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Register JWT package dependencies.
        /// </summary>
        /// <param name="services">IServiceCollection instance</param>
        /// <param name="configurationAction">Use configuration for JWT information</param>
        /// <returns></returns>
        public static IServiceCollection AddJWT(this IServiceCollection services, Action<IJWTConfiguration> configurationAction)
        {
            IJWTConfiguration configuration = new JWTConfiguration();

            configurationAction.Invoke(configuration);
            
            services.AddSingleton<IJWTConfiguration>(configuration);
            services.AddSingleton<ITokenService, JWTTokenService>();

            return services;
        }
    }
}
