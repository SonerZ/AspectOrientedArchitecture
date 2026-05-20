namespace Application.MVCUI.Services.Session;

public interface ISessionService
{
    void Add<T>(string key, T data);
    T Get<T>(string key);
    void Remove(string key);
    bool Any(string key);
}