using Entity.Models.Common;

namespace API.Controllers.CategoriesControllers.HouseholdProductCategoryController.Dtos;

public class HouseholdProductCategoryGetDto
{
    public long Id { get; set; }
    public MultiLanguageField HouseholdProductCategoryName { get; set; }
    public Guid HouseholdProductCategoryImageId  { get; set; }
    public string ImageUrl => $"https://back.easyshop.uz/File/DownloadFile/download/{HouseholdProductCategoryImageId}";
}