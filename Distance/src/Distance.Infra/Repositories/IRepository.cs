namespace Distance.Infra.Repositories;

public interface IRepository<T> where T : class
{
    Task<T?> GetAsync(string key);
    Task SetAsync(string key, T value);
}