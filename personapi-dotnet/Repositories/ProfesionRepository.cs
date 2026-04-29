using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Repositories;

public class ProfesionRepository : IProfesionRepository
{
    private readonly PersonaDbContext _context;

    public ProfesionRepository(PersonaDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Profesion>> GetAllAsync()
    {
        return await _context.Profesiones.AsNoTracking().ToListAsync();
    }

    public async Task<Profesion?> GetByIdAsync(params object[] keyValues)
    {
        return await _context.Profesiones.FindAsync(keyValues);
    }

    public async Task AddAsync(Profesion entity)
    {
        await _context.Profesiones.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Profesion entity)
    {
        _context.Profesiones.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(params object[] keyValues)
    {
        var entity = await _context.Profesiones.FindAsync(keyValues);
        if (entity is null) return;
        _context.Profesiones.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(params object[] keyValues)
    {
        return await _context.Profesiones.FindAsync(keyValues) is not null;
    }
}
