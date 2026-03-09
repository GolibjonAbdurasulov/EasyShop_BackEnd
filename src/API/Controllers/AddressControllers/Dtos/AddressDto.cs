namespace API.Controllers.AddressControllers.Dtos;

public class AddressDto
{
    public long  Id { get; set; }
    public string FullAddress { get; set; }


    public double? Latitude { get; set; }  


    public double? Longitude { get; set; }  


    public string? City { get; set; }


    public string? Region { get; set; }

    public string? PostalCode { get; set; }
        
    public long ClientId { get; set; }

}