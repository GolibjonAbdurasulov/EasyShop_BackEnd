using Entity.Models.Common;

namespace API.Controllers.ProductsControllers.WaterAndDrinkController.Dtos;

public class UpdateDto
{
    public long Id { get; set; }
    public MultiLanguageField Name { get; set; }
    public MultiLanguageField About { get; set; }
    public decimal Price { get; set; }
    public long ImageId { get; set; }
    public long MainCategoryId { get; set; }
    public long TagId { get; set; }
}