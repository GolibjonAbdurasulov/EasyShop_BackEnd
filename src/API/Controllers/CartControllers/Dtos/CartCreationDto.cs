namespace API.Controllers.CartControllers.Dtos;

public class CartCreationDto
{
    public List<long> ProductsId { get; set; }
    public long CustomerId { get; set; }
}