using Entity.Enums;
using Entity.Models.Product;

namespace API.Controllers.OrderController.Dtos;

public class OrderDetailsGetDto
{
    public long OrderId { get; set; }
    public OrderStatus Status { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime OrderedDate { get; set; }
    public List<OrderDetailsProducts> OrderDetailsProducts { get; set; }
    public string CustomerName { get; set; }
    public string CustomerPhoneNumber { get; set; }
    public double CustomerLat { get; set; }
    public double CustomerLng { get; set; }
    public string CustomerAddress { get; set; }
    
}

public class OrderDetailsProducts
{
    public long ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public int CountBox { get; set; }
}