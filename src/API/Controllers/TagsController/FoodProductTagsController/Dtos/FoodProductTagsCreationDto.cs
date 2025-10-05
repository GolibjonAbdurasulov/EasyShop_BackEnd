using Entity.Models.Common;

namespace API.Controllers.TagsController.FoodProductTagsController.Dtos;

public class FoodProductTagsCreationDto
{
    public MultiLanguageField TagName { get; set; }
}