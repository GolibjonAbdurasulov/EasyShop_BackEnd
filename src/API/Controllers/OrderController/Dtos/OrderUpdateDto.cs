using Entity.Enums;
using Entity.Models.Order;

namespace API.Controllers.OrderController.Dtos;

public class OrderUpdateDto
{
    public long Id { get; set; }
    public List<ProductItem> ProductsIds { get; set; }
    public decimal TotalPrice { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public DateTime DeliveryDate { get; set; }
    public long CustomerId { get; set; }
}