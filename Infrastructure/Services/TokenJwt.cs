using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Identity.Domain.Services;

namespace Identity.Infrastructure.Services;

public class TokenJwt : ITokenJwt
{
   public string Decrypt(string encryptedValue, string secretKey)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(secretKey);

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };

        try
        {
            var principal = tokenHandler.ValidateToken(encryptedValue, validationParameters, out _);
            var valueClaim = principal.FindFirst("value");
            return valueClaim?.Value;
        }
        catch (Exception ex)
        {
            // Tratar o erro de validação do token JWT
            // Aqui você pode logar o erro, retornar um valor padrão ou lançar uma exceção personalizada, dependendo do comportamento desejado.
            throw new Exception("Failed to decrypt JWT token.", ex);
        }
    }

    public string Encrypt(string value, string secretKey, TimeSpan tokenLifetime)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(secretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("value", value) }),
            Expires = DateTime.UtcNow.Add(tokenLifetime),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}