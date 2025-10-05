using Entity.Models.Common;

namespace API.Controllers.TagsController.OilProductTagsController.Dtos;

public class OilProductTagsCreationDto
{
    public MultiLanguageField TagName { get; set; }
}