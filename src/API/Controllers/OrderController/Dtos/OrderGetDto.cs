using Entity.Enums;
using Entity.Models.Users;

namespace API.Controllers.OrderController.Dtos;

public class OrderGetDto
{
    public long Id { get; set; }
    public List<long> ProductsIds { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public DateTime DeliveryDate { get; set; }
    public long CustomerId { get; set; }
    public virtual User User { get; set; }
}