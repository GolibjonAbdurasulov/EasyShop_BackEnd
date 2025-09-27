namespace Services.Dtos;

public class ClientDto
{
    public long Id { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public bool IsSigned { get; set; }
    public string? Token { get; set; }
}