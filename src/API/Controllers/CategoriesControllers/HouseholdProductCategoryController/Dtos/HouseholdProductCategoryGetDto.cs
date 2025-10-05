using Entity.Models.Common;

namespace API.Controllers.CategoriesControllers.HouseholdProductCategoryController.Dtos;

public class HouseholdProductCategoryGetDto
{
    public long Id { get; set; }
    public MultiLanguageField HouseholdProductCategoryName { get; set; }
    public Guid HouseholdProductCategoryImageId  { get; set; }
    public string ImageUrl => $"http://45.130.148.249:8080/File/DownloadFile/download/{HouseholdProductCategoryImageId}";
}