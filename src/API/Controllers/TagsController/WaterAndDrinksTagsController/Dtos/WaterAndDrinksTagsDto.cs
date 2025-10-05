using Entity.Models.Common;

namespace API.Controllers.TagsController.WaterAndDrinksTagsController.Dtos;

public class WaterAndDrinksTagsDto
{
    public long Id { get; set; }
    public MultiLanguageField TagName { get; set; }
}