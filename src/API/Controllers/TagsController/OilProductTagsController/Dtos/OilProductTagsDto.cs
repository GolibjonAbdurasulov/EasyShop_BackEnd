using Entity.Models.Common;

namespace API.Controllers.TagsController.OilProductTagsController.Dtos;

public class OilProductTagsDto
{
    public long Id { get; set; }
    public MultiLanguageField TagName { get; set; }   
}