using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Repositories;

public class EstudiosRepository : IEstudiosRepository
{
    private readonly PersonaDbContext _context;

    public EstudiosRepository(PersonaDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Estudios>> GetAllAsync()
    {
        return await _context.Estudios
            .AsNoTracking()
            .Include(e => e.Persona)
            .Include(e => e.Profesion)
            .ToListAsync();
    }

    public async Task<Estudios?> GetByIdAsync(params object[] keyValues)
    {
        return await _context.Estudios.FindAsync(keyValues);
    }

    public Task<Estudios?> GetByCompositeKeyAsync(int idProf, long ccPer)
    {
        return GetByIdAsync(idProf, ccPer);
    }

    public async Task AddAsync(Estudios entity)
    {
        await _context.Estudios.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Estudios entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(params object[] keyValues)
    {
        var entity = await _context.Estudios.FindAsync(keyValues);
        if (entity is null) return;
        _context.Estudios.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public Task DeleteByCompositeKeyAsync(int idProf, long ccPer)
    {
        return DeleteAsync(idProf, ccPer);
    }

    public async Task<bool> ExistsAsync(params object[] keyValues)
    {
        int idProf = (int)keyValues[0];
        long ccPer = (long)keyValues[1];
        return await _context.Estudios.AsNoTracking().AnyAsync(e => e.IdProf == idProf && e.CcPer == ccPer);
    }
}
