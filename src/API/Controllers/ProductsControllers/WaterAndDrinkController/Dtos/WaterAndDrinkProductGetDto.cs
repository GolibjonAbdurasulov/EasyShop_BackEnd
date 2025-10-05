using Entity.Models.Common;
using Entity.Models.File;
using Entity.Models.Product.Categories;
using Entity.Models.Product.Tags;

namespace API.Controllers.ProductsControllers.WaterAndDrinkController.Dtos;

public class WaterAndDrinkProductGetDto
{
    public long Id { get; set; }
    public MultiLanguageField Name { get; set; }
    public MultiLanguageField About { get; set; }
    public decimal Price { get; set; }
    public Guid ImageId { get; set; }
    public string ImageUrl => $"http://45.130.148.249:8080/File/DownloadFile/download/{ImageId}";

    public FileModel Image { get; set; }
    public long MainCategoryId { get; set; }
    public MainProductCategories MainCategory { get; set; }
    public long TagId { get; set; }
    public WaterAndDrinksTags Tag { get; set; }
    
}