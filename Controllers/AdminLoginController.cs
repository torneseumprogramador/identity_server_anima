using Microsoft.AspNetCore.Mvc;
using Identity.Domain.Entities;
using Identity.Infrastructure.Repositories.Interfaces;
using System.Threading.Tasks;
using Identity.Domain.DTOs;
using Identity.Infrastructure.Repositories;
using Identity.Domain.Services;
using Identity.Domain.ModelViews;

namespace Identity.Controllers;

[ApiController]
[Route("api/administrators")]
public class AdminLoginController : ControllerBase
{
    private readonly IRepository<Administrator> _administratorRepository;
    private readonly ITokenJwt _tokenJwt;
    private readonly ICrypto _crypto;

    public AdminLoginController(IRepository<Administrator> administratorRepository, ITokenJwt tokenJwt, ICrypto crypto)
    {
        _crypto = crypto;
        _tokenJwt = tokenJwt;
        _administratorRepository = administratorRepository;
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

        return Ok(new HttpReturn{ Message = "Administrador criado com sucesso" });
    }

    [HttpPost("/login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var administratorList = await _administratorRepository.FindAsync(a => a.Email == request.Email);
        if (administratorList.Count() == 0)
            return NotFound(new HttpReturn{ Message = "Administrador não cadastrado" });

        var administrator = administratorList.First();
        var pass = _crypto.Encrypt(request.Password, administrator.Salt);

        if(administrator.Password != pass)
            return BadRequest(new HttpReturn{ Message = "Credenciais inválidas." });

        var simpleAdministrator = SimpleAdministrator.Build(administrator);
        return Ok(new LoggedAdministrador
        { 
            Administrator = simpleAdministrator,
            Token = new AdministratorToken(_tokenJwt).BuildToken(simpleAdministrator)
        });
    }
}
