using System.ComponentModel.DataAnnotations;

namespace Services.Dtos;

public record RegisterDto
{
    
    public string? ClientFullName { get; set; }
    public string? CompanyName { get; set; }
    public string? INN { get; set; }
    [Required, Phone] public string PhoneNumber { get; set; }
    [Required, MinLength(6)] public string Password { get; set; }
    
}