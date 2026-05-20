using Newtonsoft.Json;

namespace Application.MVCUI.Services.Session;

public class SessionService : ISessionService
{
    private IHttpContextAccessor _httpContextAccessor;

    public SessionService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void Add<T>(string key, T data)
    {
        string jsonData = JsonConvert.SerializeObject(data);
        _httpContextAccessor.HttpContext.Session.SetString(key, jsonData);
    }

    public bool Any(string key)
    {
        return Get<object>(key) != null;
    }

    public T Get<T>(string key)
    {
        string jsonData = _httpContextAccessor.HttpContext.Session.GetString(key);
        return jsonData == null ? default : JsonConvert.DeserializeObject<T>(jsonData);
    }

    public void Remove(string key)
    {
        if (Any(key))
        {
            _httpContextAccessor.HttpContext.Session.Remove(key);
        }
    }
}