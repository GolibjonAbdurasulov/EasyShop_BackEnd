using Entity.Models.Common;

namespace API.Controllers.CategoriesControllers.MainProductCategoryController.Dtos;

public class MainCategoryUpdateDto
{
    public long Id { get; set; }
    public MultiLanguageField MainCategoryName { get; set; }
    public Guid MainCategoryImageId  { get; set; }
}