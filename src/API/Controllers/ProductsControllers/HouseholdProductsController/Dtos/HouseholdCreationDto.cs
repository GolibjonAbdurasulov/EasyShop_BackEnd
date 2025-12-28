using Entity.Models.Common;

namespace API.Controllers.ProductsControllers.HouseholdProductsController.Dtos;
 
public class HouseholdCreationDto
{
    public MultiLanguageField Name { get; set; }
    public MultiLanguageField About { get; set; }
    public decimal Price { get; set; }
    public Guid ImageId { get; set; }
    public long MainCategoryId { get; set; }
    public long HouseholdCategoryId { get; set; }
    public long TagId { get; set; }
    public int QuantityBoxes { get; set; }
    public int QuantityPieces { get; set; }
    public int QuantityInOneBox { get; set; }
}