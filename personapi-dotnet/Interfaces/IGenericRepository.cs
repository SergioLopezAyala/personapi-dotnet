namespace personapi_dotnet.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(params object[] keyValues);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(params object[] keyValues);
    Task<bool> ExistsAsync(params object[] keyValues);
}
