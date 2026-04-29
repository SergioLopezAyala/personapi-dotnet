using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Interfaces;

public interface IEstudiosRepository : IGenericRepository<Estudios>
{
    Task<Estudios?> GetByCompositeKeyAsync(int idProf, long ccPer);
    Task DeleteByCompositeKeyAsync(int idProf, long ccPer);
}
