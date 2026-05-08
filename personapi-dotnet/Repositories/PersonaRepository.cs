using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Repositories;

public class PersonaRepository : IPersonaRepository
{
    private readonly PersonaDbContext _context;

    public PersonaRepository(PersonaDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Persona>> GetAllAsync()
    {
        return await _context.Personas.AsNoTracking().ToListAsync();
    }

    public async Task<Persona?> GetByIdAsync(params object[] keyValues)
    {
        return await _context.Personas.FindAsync(keyValues);
    }

    public Task<Persona?> GetByCcAsync(long cc) => GetByIdAsync(cc);

    public async Task AddAsync(Persona entity)
    {
        await _context.Personas.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Persona entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(params object[] keyValues)
    {
        var entity = await _context.Personas.FindAsync(keyValues);
        if (entity is null) return;
        _context.Personas.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(params object[] keyValues)
    {
        return await _context.Personas.AsNoTracking().AnyAsync(p => p.Cc == (long)keyValues[0]);
    }
}
