using Entity.Models.Common;
using Entity.Models.File;
using Entity.Models.Product.Categories;
using Entity.Models.Product.Tags;

namespace API.Controllers.ProductsControllers.HouseholdProductsController.Dtos;

public class HouseholdGetDto
{
    public long Id { get; set; }
    public MultiLanguageField Name { get; set; }
    public MultiLanguageField About { get; set; }
    public decimal Price { get; set; }
    public Guid ImageId { get; set; }
    public string ImageUrl => $"http://45.130.148.249:8080/File/DownloadFile/download/{ImageId}";
    public long MainCategoryId { get; set; }
    public MainProductCategories MainCategory { get; set; }
    public long HouseholdProductCategoryId { get; set; }
    public HouseholdProductCategory HouseholdProductCategory { get; set; }
    public long TagId { get; set; }
    public HouseholdProductTags Tag { get; set; }
    
}