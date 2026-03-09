using API.Controllers.AddressControllers.Dtos;
using Entity.Enums;

namespace API.Controllers.OrderController.Dtos;

public class ShippedOrdersDtoToDelivery
{
    public long Id { get; set; }
    public long CustomerId { get; set; }
    public decimal TotalPrice { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public DateTime DeliveryDate { get; set; }
    public AddressDtoToDelivery OrderAddress { get; set; }
}

public class AddressDtoToDelivery
{
    public long  Id { get; set; }
    public string FullAddress { get; set; }
    public double? Latitude { get; set; }  
    public double? Longitude { get; set; }  
    
}