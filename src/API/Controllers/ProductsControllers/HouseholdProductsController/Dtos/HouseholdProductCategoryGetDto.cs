using Entity.Models.Common;

namespace API.Controllers.ProductsControllers.HouseholdProductsController.Dtos;

public class HouseholdProductCategoryGetDto
{
    public long Id { get; set; }
    public MultiLanguageField Name { get; set; }
    public MultiLanguageField About { get; set; }
    public decimal Price { get; set; }
    public Guid ImageId { get; set; }
    public string ImageUrl => $"https://back.easyshop.uz/File/DownloadFile/download/{ImageId}";
    public long MainCategoryId { get; set; }
    public long HouseholdProductCategoryId { get; set; }
    public long WarehouseDatesId { get; set; }  

    public long TagId { get; set; }
}