using Entity.Models.Common;

namespace API.Controllers.ProductsControllers.FoodProductController.Dtos;

public class CreationDto
{
    public MultiLanguageField Name { get; set; }
    public MultiLanguageField About { get; set; }
    public decimal Price { get; set; }
    public long ImageId { get; set; }
    public long MainCategoryId { get; set; }
    public long FoodProductCategoryId { get; set; }
    public long TagId { get; set; }

}