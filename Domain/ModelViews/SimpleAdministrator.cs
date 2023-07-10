using System.ComponentModel.DataAnnotations;
using Identity.Domain.Entities;

namespace Identity.Domain.ModelViews;
public class SimpleAdministrator
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public string Email { get; set; } = default!;

    public static SimpleAdministrator Build(Administrator administrator)
    {
        return new SimpleAdministrator
        { 
            Id = administrator.Id, 
            Email = administrator.Email, 
            Name = administrator.Name 
        };
    }
}
