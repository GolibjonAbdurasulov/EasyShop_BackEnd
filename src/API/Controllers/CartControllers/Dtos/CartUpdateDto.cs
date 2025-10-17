using Entity.Models.Order;

namespace API.Controllers.CartControllers.Dtos;

public class CartUpdateDto
{
    public long Id { get; set; }
    public List<ProductItem> ProductsId { get; set; }
    public long CustomerId { get; set; }
}