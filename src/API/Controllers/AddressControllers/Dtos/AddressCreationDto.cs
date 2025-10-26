namespace API.Controllers.AddressControllers.Dtos;

public class AddressCreationDto
{

    public string FullAddress { get; set; }

    // 🌍 Geolokatsiya koordinatalari

    public double? Latitude { get; set; }   // kenglik (masalan: 41.2995)


    public double? Longitude { get; set; }  // uzunlik (masalan: 69.2401)

    // 🏙 Shahar va mintaqa ma’lumotlari

    public string? City { get; set; }


    public string? Region { get; set; }

    public string? PostalCode { get; set; }
        
    public long ClientId { get; set; }

}