namespace API.Controllers.CartControllers.Dtos;

public class DeleteProductFromCartDto
{
    public long CartId { get; set; }
    public string ProductType { get; set; }
    public long ProductId { get; set; }
}