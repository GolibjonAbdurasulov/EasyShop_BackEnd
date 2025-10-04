using Entity.Models.Common;
using Entity.Models.File;

namespace API.Controllers.CategoriesControllers.MainProductCategoryController.Dtos;

public class MainCategoryGetDto
{
    public long Id { get; set; }
    public MultiLanguageField MainCategoryName { get; set; }
    public Guid MainCategoryImageId  { get; set; }
}