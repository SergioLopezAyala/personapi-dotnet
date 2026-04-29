using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers.Api;

[ApiController]
[Route("api/estudios")]
[Produces("application/json")]
public class EstudiosApiController : ControllerBase
{
    private readonly IEstudiosRepository _repository;

    public EstudiosApiController(IEstudiosRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Estudios>>> GetAll()
    {
        var items = await _repository.GetAllAsync();
        return Ok(items);
    }

    // Clave compuesta (id_prof, cc_per)
    [HttpGet("{idProf:int}/{ccPer:long}")]
    public async Task<ActionResult<Estudios>> GetById(int idProf, long ccPer)
    {
        var item = await _repository.GetByCompositeKeyAsync(idProf, ccPer);
        if (item is null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<Estudios>> Create([FromBody] Estudios estudios)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (await _repository.ExistsAsync(estudios.IdProf, estudios.CcPer))
            return Conflict("Ya existe un registro con la misma clave compuesta.");

        await _repository.AddAsync(estudios);
        return CreatedAtAction(nameof(GetById),
            new { idProf = estudios.IdProf, ccPer = estudios.CcPer }, estudios);
    }

    [HttpPut("{idProf:int}/{ccPer:long}")]
    public async Task<ActionResult> Update(int idProf, long ccPer, [FromBody] Estudios estudios)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (idProf != estudios.IdProf || ccPer != estudios.CcPer)
            return BadRequest("La clave compuesta de la URL no coincide con el cuerpo.");
        if (!await _repository.ExistsAsync(idProf, ccPer)) return NotFound();

        await _repository.UpdateAsync(estudios);
        return NoContent();
    }

    [HttpDelete("{idProf:int}/{ccPer:long}")]
    public async Task<ActionResult> Delete(int idProf, long ccPer)
    {
        if (!await _repository.ExistsAsync(idProf, ccPer)) return NotFound();
        await _repository.DeleteByCompositeKeyAsync(idProf, ccPer);
        return NoContent();
    }
}
