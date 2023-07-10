namespace Identity.Domain.Services;

public interface ICrypto
{
    string GetSalt();
    string Encrypt(string value, string salt);
}