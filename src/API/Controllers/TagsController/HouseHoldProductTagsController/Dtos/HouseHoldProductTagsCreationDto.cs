using Entity.Models.Common;

namespace API.Controllers.TagsController.HouseHoldProductTagsController.Dtos;

public class HouseHoldProductTagsCreationDto
{
    public MultiLanguageField TagName { get; set; }
    public long  CategoryId { get; set; }

}