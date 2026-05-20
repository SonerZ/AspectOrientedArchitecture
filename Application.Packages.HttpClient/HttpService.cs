using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace Application.Packages.HttpClientService;

public class HttpService : IHttpService
{
    public async Task<T> GetAsync<T>(string url)
    {
        using var httpClient = new System.Net.Http.HttpClient();
        
        var response = await httpClient.GetAsync(url);
        var responseValueAsString = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<T>(responseValueAsString)!;
    }

    public async Task<T> PostAsync<T>(string url, object data)
    {
        using var httpClient = new System.Net.Http.HttpClient();
        var requestDataAsJson = JsonConvert.SerializeObject(data);
        var stringContent = new StringContent(requestDataAsJson, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync(url, stringContent);
        var responseValueAsString = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<T>(responseValueAsString)!;
    }
    
    public async Task<T> GetAsync<T>(string url, string token)
    {
        var httpClient = new System.Net.Http.HttpClient();

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        var response = await httpClient.GetAsync(url);

        var responseValueAsString = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<T>(responseValueAsString);
    }

    public async Task<T> PostAsync<T>(string url, object data, string token)
    {
        var httpClient = new System.Net.Http.HttpClient();
        
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var requestDataAsJson = JsonConvert.SerializeObject(data);
        var stringContent = new StringContent(requestDataAsJson, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(url, stringContent);

        var responseValueAsString = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<T>(responseValueAsString);
    }
}