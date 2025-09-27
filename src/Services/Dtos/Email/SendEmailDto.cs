using System.ComponentModel.DataAnnotations;

namespace Services.Dtos.Email;

public record SendEmailDto
{
    [EmailAddress] public string Email { get; set; }
    public string? Subject { get; set; }
    [MinLength(5), Required] public string Message { get; set; }
}