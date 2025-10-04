using Entity.Models.Common;

namespace API.Controllers.CategoriesControllers.FoodProductCategoryController.Dtos;

public class FoodProductCategoryCreationDto
{
    public MultiLanguageField FoodProductCategoryName { get; set; }
    public Guid FoodProductCategoryImageId  { get; set; }
}