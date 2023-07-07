using Microsoft.AspNetCore.Mvc;
using Identity.Domain.Entities;
using Identity.Infrastructure.Repositories.Interfaces;
using System.Threading.Tasks;
using Identity.Domain.DTOs;

namespace Identity.Controllers;

[ApiController]
[Route("api/administrator")]
public class AdminLoginController : ControllerBase
{
    private readonly IRepository<Administrator> _administratorRepository;

    public AdminLoginController(IRepository<Administrator> administratorRepository)
    {
        _administratorRepository = administratorRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        // Verificar se o administrador existe e a senha está correta
        var administrator = await _administratorRepository.FindAsync(a => a.Email == request.Email && a.Password == request.Password);
        if (administrator == null)
        {
            return BadRequest("Credenciais inválidas.");
        }

        return Ok("Login bem-sucedido!");
    }
}
