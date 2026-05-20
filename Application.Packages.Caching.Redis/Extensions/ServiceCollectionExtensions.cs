using System;
using Application.Packages.Caching.Redis.Configuration;
using Application.Packages.Caching.Redis.Server;
using Application.Packages.Caching.Redis.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Packages.Caching.Redis.Extensions
{
    /// <summary>
    /// Add Redis cache service dependencies
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Register Redis dependencies to IServiceCollection
        /// </summary>
        /// <param name="services">IServiceCollection instance</param>
        /// <param name="configuration">RedisConfiguration instance</param>
        /// <returns></returns>
        public static IServiceCollection AddRedis(this IServiceCollection services, Action<IRedisConfiguration> configuration)
        {
            IRedisConfiguration redisConfiguration = new RedisConfiguration();
            configuration.Invoke(redisConfiguration);

            services.AddSingleton<IRedisConfiguration>(redisConfiguration);

            services.AddSingleton<IRedisCacheService, RedisCacheService>();

            services.AddSingleton<IRedisServer, RedisServer>();

            return services;
        }
    }
}
