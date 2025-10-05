using Entity.Models.Common;

namespace API.Controllers.TagsController.FoodProductTagsController.Dtos;

public class FoodProductTagsDto
{
    public long Id { get; set; }
    public MultiLanguageField TagName { get; set; }
}