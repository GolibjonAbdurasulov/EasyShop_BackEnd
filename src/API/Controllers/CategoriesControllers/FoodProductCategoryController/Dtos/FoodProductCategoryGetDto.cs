using Entity.Models.Common;

namespace API.Controllers.CategoriesControllers.FoodProductCategoryController.Dtos;

public class FoodProductCategoryGetDto
{
    public long Id { get; set; }
    public MultiLanguageField FoodProductCategoryName { get; set; }
    public Guid FoodProductCategoryImageId  { get; set; }
}