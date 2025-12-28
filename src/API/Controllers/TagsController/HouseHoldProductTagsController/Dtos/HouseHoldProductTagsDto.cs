using Entity.Models.Common;

namespace API.Controllers.TagsController.HouseHoldProductTagsController.Dtos;

public class HouseHoldProductTagsDto
{
    public long Id { get; set; }
    public MultiLanguageField TagName { get; set; }
    public long  CategoryId { get; set; }

}