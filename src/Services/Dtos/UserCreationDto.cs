namespace Services.Dtos;

public class UserCreationDto
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}