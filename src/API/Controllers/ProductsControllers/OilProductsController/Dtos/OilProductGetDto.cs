using Entity.Models.Common;
using Entity.Models.File;
using Entity.Models.Product.Categories;
using Entity.Models.Product.Tags;

namespace API.Controllers.ProductsControllers.OilProductsController.Dtos;

public class OilProductGetDto
{
    public long Id { get; set; }
    public MultiLanguageField Name { get; set; }
    public MultiLanguageField About { get; set; }
    public decimal Price { get; set; }
    public Guid ImageId { get; set; }
    public FileModel Image { get; set; }
    public long MainCategoryId { get; set; }
    public MainProductCategories MainCategory { get; set; }
    public long TagId { get; set; }
    public OilProductTags Tag { get; set; }
    
}