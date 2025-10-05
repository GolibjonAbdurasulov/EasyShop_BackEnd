using Entity.Models.Common;

namespace API.Controllers.CategoriesControllers.FoodProductCategoryController.Dtos;

public class FoodProductCategoryGetDto
{
    public long Id { get; set; }
    public MultiLanguageField FoodProductCategoryName { get; set; }
    public Guid FoodProductCategoryImageId  { get; set; }
    public string ImageUrl => $"http://45.130.148.249:8080/File/DownloadFile/download/{FoodProductCategoryImageId}";

}