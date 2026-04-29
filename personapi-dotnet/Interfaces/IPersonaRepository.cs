using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Interfaces;

public interface IPersonaRepository : IGenericRepository<Persona>
{
    Task<Persona?> GetByCcAsync(long cc);
}
