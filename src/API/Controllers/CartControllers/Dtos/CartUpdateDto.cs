namespace API.Controllers.CartControllers.Dtos;

public class CartUpdateDto
{
    public long Id { get; set; }
    public List<long> ProductsId { get; set; }
    public long CustomerId { get; set; }
}