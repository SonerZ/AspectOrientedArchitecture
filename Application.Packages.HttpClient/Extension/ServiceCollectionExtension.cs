using Microsoft.Extensions.DependencyInjection;
using Application.Packages.HttpClientService;

namespace Application.Packages.HttpClient.Extension
{
    public static class ServiceCollectionExtension
    {
        public static void AddHttpClientService(this IServiceCollection services)
        {
            services.AddSingleton<IHttpService, HttpService>();
        }
    }
}
