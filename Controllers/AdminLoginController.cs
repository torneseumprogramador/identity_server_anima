using Microsoft.AspNetCore.Mvc;
using Identity.Domain.Entities;
using Identity.Infrastructure.Repositories.Interfaces;
using System.Threading.Tasks;
using Identity.Domain.DTOs;
using Identity.Infrastructure.Repositories;
using Identity.Domain.Services;

namespace Identity.Controllers;

[ApiController]
[Route("api/administrator")]
public class AdminLoginController : ControllerBase
{
    private readonly IRepository<Administrator> _administratorRepository;
    private readonly ICrypto _crypto;

    public AdminLoginController(AppContext appContext, ICrypto crypto)
    // public AdminLoginController(IRepository<Administrator> administratorRepository)
    {
        _crypto = crypto;
        _administratorRepository = new Repository<Administrator>(appContext);
    }

    [HttpGet("/insert")]
    public async Task<IActionResult> Insert()
    {
        var salt = _crypto.GetSalt();
        var adm = new Administrator(){
            Name = "Danilo",
            Email = "danilo@teste.com",
            Password = _crypto.Encrypt("asds", salt),
            Salt = salt
        };

        await _administratorRepository.AddAsync(adm);

        return Ok("Cadastrado com sucesso");
    }

    [HttpPost("/")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var administrator = (await _administratorRepository.FindAsync(a => a.Email == request.Email)).First();
        if (administrator == null)
            return BadRequest("Credenciais inválidas.");

        var pass = _crypto.Encrypt(request.Password, administrator.Salt);

        if(administrator.Password != pass)
            return BadRequest("Credenciais inválidas.");

        return Ok("Login bem-sucedido!");
    }
}
