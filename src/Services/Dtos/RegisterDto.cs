using System.ComponentModel.DataAnnotations;

namespace Services.Dtos;

public record RegisterDto
{
    [Required, Phone] public string PhoneNumber { get; set; }
    [Required, MinLength(6)] public string Password { get; set; }
    public string FullName { get; set; }
}