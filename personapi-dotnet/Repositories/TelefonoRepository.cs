using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Repositories;

public class TelefonoRepository : ITelefonoRepository
{
    private readonly PersonaDbContext _context;

    public TelefonoRepository(PersonaDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Telefono>> GetAllAsync()
    {
        return await _context.Telefonos
            .AsNoTracking()
            .Include(t => t.Persona)
            .ToListAsync();
    }

    public async Task<Telefono?> GetByIdAsync(params object[] keyValues)
    {
        return await _context.Telefonos.FindAsync(keyValues);
    }

    public Task<Telefono?> GetByNumAsync(string num) => GetByIdAsync(num);

    public async Task AddAsync(Telefono entity)
    {
        await _context.Telefonos.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Telefono entity)
    {
        _context.Telefonos.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(params object[] keyValues)
    {
        var entity = await _context.Telefonos.FindAsync(keyValues);
        if (entity is null) return;
        _context.Telefonos.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(params object[] keyValues)
    {
        return await _context.Telefonos.FindAsync(keyValues) is not null;
    }
}
