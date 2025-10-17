using Entity.Models.Order;

namespace API.Controllers.CartControllers.Dtos;

public class CartCreationDto
{
    public List<ProductItem> ProductsId { get; set; }
    public long CustomerId { get; set; }
}