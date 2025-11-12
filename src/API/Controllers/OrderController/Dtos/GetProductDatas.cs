using Entity.Models.Order;

namespace API.Controllers.OrderController.Dtos;

public class GetProductDatas
{
    public List<ProductItem> productsItemIds { get; set; }
}