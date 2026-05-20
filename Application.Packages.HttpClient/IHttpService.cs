using System.Threading.Tasks;

namespace Application.Packages.HttpClientService;

public interface IHttpService
{
    Task<T> GetAsync<T>(string url);
    Task<T> PostAsync<T>(string url, object data);
    Task<T> PostAsync<T>(string url, object data, string token);
    Task<T> GetAsync<T>(string url, string token);
}