using Entity.Models.Common;
using Entity.Models.File;
using Entity.Models.Product.Categories;
using Entity.Models.Product.Tags;

namespace API.Controllers.ProductsControllers.HouseholdProductsController.Dtos;

public class GetDto
{
    public long Id { get; set; }
    public MultiLanguageField Name { get; set; }
    public MultiLanguageField About { get; set; }
    public decimal Price { get; set; }
    public long ImageId { get; set; }
    public FileModel Image { get; set; }
    public long CategoryImageId { get; set; }
    public FileModel CategoryImage { get; set; }
    public long CategoryId { get; set; }
    public HouseholdProductCategory FoodProductCategory { get; set; }
    public long TagId { get; set; }
    public HouseholdProductTags Tag { get; set; }
    
}