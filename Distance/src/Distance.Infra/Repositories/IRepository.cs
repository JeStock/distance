namespace Distance.Infra.Repositories;

public interface IRepository<T> where T : class
{
    Task<T?> GetAsync(string key, CancellationToken token = default);
    Task SetAsync(string key, T value, CancellationToken token = default);
}