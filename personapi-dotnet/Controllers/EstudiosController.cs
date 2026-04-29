using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers;

public class EstudiosController : Controller
{
    private readonly IEstudiosRepository _repository;

    public EstudiosController(IEstudiosRepository repository)
    {
        _repository = repository;
    }

    public async Task<IActionResult> Index()
    {
        var items = await _repository.GetAllAsync();
        return View(items);
    }

    public async Task<IActionResult> Details(int? idProf, long? ccPer)
    {
        if (idProf is null || ccPer is null) return NotFound();
        var item = await _repository.GetByCompositeKeyAsync(idProf.Value, ccPer.Value);
        if (item is null) return NotFound();
        return View(item);
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Estudios estudios)
    {
        if (!ModelState.IsValid) return View(estudios);
        if (await _repository.ExistsAsync(estudios.IdProf, estudios.CcPer))
        {
            ModelState.AddModelError(string.Empty, "Ya existe un registro con la misma clave compuesta.");
            return View(estudios);
        }
        await _repository.AddAsync(estudios);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? idProf, long? ccPer)
    {
        if (idProf is null || ccPer is null) return NotFound();
        var item = await _repository.GetByCompositeKeyAsync(idProf.Value, ccPer.Value);
        if (item is null) return NotFound();
        return View(item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int idProf, long ccPer, Estudios estudios)
    {
        if (idProf != estudios.IdProf || ccPer != estudios.CcPer) return NotFound();
        if (!ModelState.IsValid) return View(estudios);
        await _repository.UpdateAsync(estudios);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? idProf, long? ccPer)
    {
        if (idProf is null || ccPer is null) return NotFound();
        var item = await _repository.GetByCompositeKeyAsync(idProf.Value, ccPer.Value);
        if (item is null) return NotFound();
        return View(item);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int idProf, long ccPer)
    {
        await _repository.DeleteByCompositeKeyAsync(idProf, ccPer);
        return RedirectToAction(nameof(Index));
    }
}
