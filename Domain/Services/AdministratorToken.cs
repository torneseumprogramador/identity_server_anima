using System.Text.Json;
using Identity.Domain.ModelViews;

namespace Identity.Domain.Services;

public class AdministratorToken
{
    public AdministratorToken(ITokenJwt tokenJwt)
    {
        _tokenJwt = tokenJwt;
    }

    private ITokenJwt _tokenJwt;
    public string BuildToken(SimpleAdministrator administrator)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

        TimeSpan time = TimeSpan.FromDays(Convert.ToInt16(configuration["TimeJwt"]));
        var secret = configuration["Secret"];
        var jsonAdm = JsonSerializer.Serialize(administrator);
        return _tokenJwt.Encrypt(jsonAdm, secret, time);
    }
}