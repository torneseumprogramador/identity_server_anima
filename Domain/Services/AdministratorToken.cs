using System.Text.Json;
using Identity.Domain.ModelViews;

namespace Identity.Domain.Services;

public class AdministratorToken
{
    public AdministratorToken(ITokenJwt tokenJwt)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

        _time = TimeSpan.FromDays(Convert.ToInt16(configuration["TimeJwt"]));
        _secret = configuration["Secret"];
        _tokenJwt = tokenJwt;
    }

    private TimeSpan _time;
    private string _secret;
    private ITokenJwt _tokenJwt;

    public string BuildToken(SimpleAdministrator administrator)
    {
        var jsonAdm = JsonSerializer.Serialize(administrator);
        return _tokenJwt.Encrypt(jsonAdm, _secret, _time);
    }

    public SimpleAdministrator TokenToAdm(string token)
    {
        try
        {
            var jsonAdm = _tokenJwt.Decrypt(token, _secret);
            return JsonSerializer.Deserialize<SimpleAdministrator>(jsonAdm);
        }
        catch
        {
            return null;
        }
    }
}