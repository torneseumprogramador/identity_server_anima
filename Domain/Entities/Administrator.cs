using System.ComponentModel.DataAnnotations;

namespace Identity.Domain.Entities;
public class Administrator
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(150)]
    public string Name { get; set; } = default!;

    [Required]
    [MaxLength(200)]
    public string Email { get; set; } = default!;

    [Required]
    [MaxLength(255)]
    public string Password { get; set; } = default!;

    [Required]
    [MaxLength(255)]
    public string Salt { get; set; } = default!;
}
