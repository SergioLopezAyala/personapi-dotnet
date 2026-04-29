using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers.Api;

[ApiController]
[Route("api/telefono")]
[Produces("application/json")]
public class TelefonoApiController : ControllerBase
{
    private readonly ITelefonoRepository _repository;

    public TelefonoApiController(ITelefonoRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Telefono>>> GetAll()
    {
        var items = await _repository.GetAllAsync();
        return Ok(items);
    }

    [HttpGet("{num}")]
    public async Task<ActionResult<Telefono>> GetById(string num)
    {
        var item = await _repository.GetByNumAsync(num);
        if (item is null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<Telefono>> Create([FromBody] Telefono telefono)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (await _repository.ExistsAsync(telefono.Num))
            return Conflict($"Ya existe un teléfono con num={telefono.Num}.");

        await _repository.AddAsync(telefono);
        return CreatedAtAction(nameof(GetById), new { num = telefono.Num }, telefono);
    }

    [HttpPut("{num}")]
    public async Task<ActionResult> Update(string num, [FromBody] Telefono telefono)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (num != telefono.Num) return BadRequest("El num en URL no coincide con el del cuerpo.");
        if (!await _repository.ExistsAsync(num)) return NotFound();

        await _repository.UpdateAsync(telefono);
        return NoContent();
    }

    [HttpDelete("{num}")]
    public async Task<ActionResult> Delete(string num)
    {
        if (!await _repository.ExistsAsync(num)) return NotFound();
        await _repository.DeleteAsync(num);
        return NoContent();
    }
}
