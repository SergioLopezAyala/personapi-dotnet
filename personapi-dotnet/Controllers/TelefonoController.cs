using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers;

public class TelefonoController : Controller
{
    private readonly ITelefonoRepository _repository;

    public TelefonoController(ITelefonoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IActionResult> Index()
    {
        var items = await _repository.GetAllAsync();
        return View(items);
    }

    public async Task<IActionResult> Details(string id)
    {
        if (string.IsNullOrEmpty(id)) return NotFound();
        var item = await _repository.GetByNumAsync(id);
        if (item is null) return NotFound();
        return View(item);
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Telefono telefono)
    {
        if (!ModelState.IsValid) return View(telefono);
        if (await _repository.ExistsAsync(telefono.Num))
        {
            ModelState.AddModelError(nameof(Telefono.Num), "Ya existe un teléfono con ese número.");
            return View(telefono);
        }
        await _repository.AddAsync(telefono);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(string id)
    {
        if (string.IsNullOrEmpty(id)) return NotFound();
        var item = await _repository.GetByNumAsync(id);
        if (item is null) return NotFound();
        return View(item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, Telefono telefono)
    {
        if (id != telefono.Num) return NotFound();
        if (!ModelState.IsValid) return View(telefono);
        await _repository.UpdateAsync(telefono);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(string id)
    {
        if (string.IsNullOrEmpty(id)) return NotFound();
        var item = await _repository.GetByNumAsync(id);
        if (item is null) return NotFound();
        return View(item);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        await _repository.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
