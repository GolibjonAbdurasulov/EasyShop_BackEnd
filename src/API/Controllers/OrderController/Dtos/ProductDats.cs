using Entity.Models.Product;

namespace API.Controllers.OrderController.Dtos;

public class ProductDats
{
    public string Name { get; set; }
    public string About { get; set; }
    public decimal Price { get; set; }
}