using Entity.Models.Common;

namespace API.Controllers.ProductsControllers.HouseholdProductsController.Dtos;

public class UpdateDto
{
    public long Id { get; set; }
    public MultiLanguageField Name { get; set; }
    public MultiLanguageField About { get; set; }
    public decimal Price { get; set; }
    public long ImageId { get; set; }
    public long CategoryImageId { get; set; }
    public long CategoryId { get; set; }
    public long TagId { get; set; }
}