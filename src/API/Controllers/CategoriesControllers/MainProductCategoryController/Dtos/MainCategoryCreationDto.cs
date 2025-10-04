using Entity.Models.Common;

namespace API.Controllers.CategoriesControllers.MainProductCategoryController.Dtos;

public class MainCategoryCreationDto
{
    public MultiLanguageField MainCategoryName { get; set; }
    public Guid MainCategoryImageId  { get; set; }
}