namespace API.Controllers.ClientController.Dtos;

public class ClientCreationDto
{
    public string FullName { get; set; }   
    public string PhoneNumber { get; set; }   
    public string Password { get; set; }   
    public bool IsSigned { get; set; }
}