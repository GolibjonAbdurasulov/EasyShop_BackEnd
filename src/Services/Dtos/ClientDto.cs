namespace Services.Dtos;

public class ClientDto
{
    public long Id { get; set; }
    public string? ClientFullName { get; set; }
    public string? CompanyName { get; set; }
    public string? INN { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Password { get; set; }
    public bool IsSigned { get; set; }
    public string? Token { get; set; }
}