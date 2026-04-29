using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers;

public class ProfesionController : Controller
{
    private readonly IProfesionRepository _repository;

    public ProfesionController(IProfesionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IActionResult> Index()
    {
        var items = await _repository.GetAllAsync();
        return View(items);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null) return NotFound();
        var item = await _repository.GetByIdAsync(id.Value);
        if (item is null) return NotFound();
        return View(item);
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Profesion profesion)
    {
        if (!ModelState.IsValid) return View(profesion);
        await _repository.AddAsync(profesion);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();
        var item = await _repository.GetByIdAsync(id.Value);
        if (item is null) return NotFound();
        return View(item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Profesion profesion)
    {
        if (id != profesion.Id) return NotFound();
        if (!ModelState.IsValid) return View(profesion);
        await _repository.UpdateAsync(profesion);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();
        var item = await _repository.GetByIdAsync(id.Value);
        if (item is null) return NotFound();
        return View(item);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _repository.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
