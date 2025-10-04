using Entity.Models.Common;

namespace API.Controllers.CategoriesControllers.HouseholdProductCategoryController.Dtos;

public class HouseholdProductCategoryUpdateDto
{
    public long Id { get; set; }
    public MultiLanguageField HouseholdProductCategoryName { get; set; }
    public Guid HouseholdProductCategoryImageId  { get; set; }
}