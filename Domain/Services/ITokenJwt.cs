namespace Identity.Domain.Services;

public interface ITokenJwt
{
    string Decrypt(string encryptValue, string secretKey);
    string Encrypt(string value, string secretKey, TimeSpan tokenLifetime);
}