using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Interfaces;

public interface ITelefonoRepository : IGenericRepository<Telefono>
{
    Task<Telefono?> GetByNumAsync(string num);
}
