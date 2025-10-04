using Entity.Models.Common;

namespace API.Controllers.ProductsControllers.OilProductsController.Dtos;

public class OilProductUpdateDto
{
    public long Id { get; set; }
    public MultiLanguageField Name { get; set; }
    public MultiLanguageField About { get; set; }
    public decimal Price { get; set; }
    public Guid ImageId { get; set; }
    public long MainCategoryId { get; set; }
    public long TagId { get; set; }
}