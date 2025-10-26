using Entity.Enums;
using Entity.Models.Client;
using Entity.Models.Order;
using Entity.Models.Users;

namespace API.Controllers.OrderController.Dtos;

public class OrderGetDto
{
    public long Id { get; set; }
    public List<ProductItem> ProductsIds { get; set; }
    public decimal TotalPrice { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public DateTime DeliveryDate { get; set; }
    public long CustomerId { get; set; }
    public virtual Client User { get; set; }
}