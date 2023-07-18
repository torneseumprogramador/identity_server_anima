using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Identity.Infrastructure.Repositories;
using Identity.Domain.Entities;
using Identity.Domain.Filters;
using Identity.Infrastructure.Repositories.Interfaces;

namespace identity_server_anima.Controllers;

[Authorize]
[Route("api/administrators")]
[ApiController]
public class AdministratorsController : ControllerBase
{
    private readonly IRepository<Administrator> _repository;

    public AdministratorsController(IRepository<Administrator> repository)
    {
        _repository = repository;
    }

    // GET: api/Administrators
    [HttpGet]
    public async Task<IEnumerable<Administrator>> GetAdministrators()
    {
        return await _repository.GetAllAsync();
    }

    // GET: api/Administrators/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Administrator>> GetAdministrator(int id)
    {
        var administrator = await _repository.GetByIdAsync(id);

        if (administrator == null)
        {
            return NotFound();
        }

        return administrator;
    }

    // PUT: api/Administrators/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAdministrator(int id, Administrator administrator)
    {
        if (id != administrator.Id)
        {
            return BadRequest();
        }

        await _repository.UpdateAsync(administrator);

        return NoContent();
    }

    // POST: api/Administrators
    [HttpPost]
    public async Task<ActionResult<Administrator>> PostAdministrator(Administrator administrator)
    {
        await _repository.AddAsync(administrator);

        return CreatedAtAction("GetAdministrator", new { id = administrator.Id }, administrator);
    }

    // DELETE: api/Administrators/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAdministrator(int id)
    {
        var administrator = await _repository.GetByIdAsync(id);
        if (administrator == null)
        {
            return NotFound();
        }

        await _repository.RemoveAsync(administrator);

        return NoContent();
    }
}
