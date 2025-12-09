namespace API.Controllers.ClientController.Dtos;

public class ClientCreationDto
{
    public string? ClientFullName { get; set; }
    public string? CompanyName { get; set; }
    public string? INN { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Password { get; set; }
}