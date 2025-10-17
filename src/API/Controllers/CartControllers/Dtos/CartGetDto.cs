using Entity.Models.Order;
using Entity.Models.Users;

namespace API.Controllers.CartControllers.Dtos;

public class CartGetDto
{
    public long Id { get; set; }
    public List<ProductItem> ProductsId { get; set; }
    public long CustomerId { get; set; }
    public virtual User Customer { get; set; }
}