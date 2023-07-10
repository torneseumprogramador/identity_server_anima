
namespace Identity.Domain.DTOs;

public record LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}