using Entity.Models.Common;

namespace API.Controllers.CategoriesControllers.HouseholdProductCategoryController.Dtos;

public class HouseholdProductCategoryCreationDto
{
    public MultiLanguageField HouseholdProductCategoryName { get; set; }
    public Guid HouseholdProductCategoryImageId  { get; set; }
}