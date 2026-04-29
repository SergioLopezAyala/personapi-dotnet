using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers.Api;

[ApiController]
[Route("api/profesion")]
[Produces("application/json")]
public class ProfesionApiController : ControllerBase
{
    private readonly IProfesionRepository _repository;

    public ProfesionApiController(IProfesionRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Profesion>>> GetAll()
    {
        var items = await _repository.GetAllAsync();
        return Ok(items);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Profesion>> GetById(int id)
    {
        var item = await _repository.GetByIdAsync(id);
        if (item is null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<Profesion>> Create([FromBody] Profesion profesion)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        await _repository.AddAsync(profesion);
        return CreatedAtAction(nameof(GetById), new { id = profesion.Id }, profesion);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] Profesion profesion)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (id != profesion.Id) return BadRequest("El id en URL no coincide con el id del cuerpo.");
        if (!await _repository.ExistsAsync(id)) return NotFound();

        await _repository.UpdateAsync(profesion);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        if (!await _repository.ExistsAsync(id)) return NotFound();
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}
