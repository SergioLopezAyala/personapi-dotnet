using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers.Api;

[ApiController]
[Route("api/persona")]
[Produces("application/json")]
public class PersonaApiController : ControllerBase
{
    private readonly IPersonaRepository _repository;

    public PersonaApiController(IPersonaRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Persona>>> GetAll()
    {
        var items = await _repository.GetAllAsync();
        return Ok(items);
    }

    [HttpGet("{cc:long}")]
    public async Task<ActionResult<Persona>> GetById(long cc)
    {
        var item = await _repository.GetByCcAsync(cc);
        if (item is null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<Persona>> Create([FromBody] Persona persona)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (await _repository.ExistsAsync(persona.Cc))
            return Conflict($"Ya existe una persona con cc={persona.Cc}.");

        await _repository.AddAsync(persona);
        return CreatedAtAction(nameof(GetById), new { cc = persona.Cc }, persona);
    }

    [HttpPut("{cc:long}")]
    public async Task<ActionResult> Update(long cc, [FromBody] Persona persona)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (cc != persona.Cc) return BadRequest("La cc en URL no coincide con la cc del cuerpo.");
        if (!await _repository.ExistsAsync(cc)) return NotFound();

        await _repository.UpdateAsync(persona);
        return NoContent();
    }

    [HttpDelete("{cc:long}")]
    public async Task<ActionResult> Delete(long cc)
    {
        if (!await _repository.ExistsAsync(cc)) return NotFound();
        await _repository.DeleteAsync(cc);
        return NoContent();
    }
}
