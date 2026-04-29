using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers;

public class PersonaController : Controller
{
    private readonly IPersonaRepository _repository;

    public PersonaController(IPersonaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IActionResult> Index()
    {
        var items = await _repository.GetAllAsync();
        return View(items);
    }

    public async Task<IActionResult> Details(long? id)
    {
        if (id is null) return NotFound();
        var item = await _repository.GetByCcAsync(id.Value);
        if (item is null) return NotFound();
        return View(item);
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Persona persona)
    {
        if (!ModelState.IsValid) return View(persona);
        if (await _repository.ExistsAsync(persona.Cc))
        {
            ModelState.AddModelError(nameof(Persona.Cc), "Ya existe una persona con esa cédula.");
            return View(persona);
        }
        await _repository.AddAsync(persona);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(long? id)
    {
        if (id is null) return NotFound();
        var item = await _repository.GetByCcAsync(id.Value);
        if (item is null) return NotFound();
        return View(item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(long id, Persona persona)
    {
        if (id != persona.Cc) return NotFound();
        if (!ModelState.IsValid) return View(persona);
        await _repository.UpdateAsync(persona);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(long? id)
    {
        if (id is null) return NotFound();
        var item = await _repository.GetByCcAsync(id.Value);
        if (item is null) return NotFound();
        return View(item);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(long id)
    {
        await _repository.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
