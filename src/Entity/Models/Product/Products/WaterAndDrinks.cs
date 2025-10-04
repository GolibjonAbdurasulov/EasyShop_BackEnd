using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Product.Tags;

namespace Entity.Models.Product.Products;
[Table("water_and_drinks")]
public class WaterAndDrinks : Product
{
    [Column("tag_id"),ForeignKey(nameof(Tag))]public long TagId { get; set; }
    public virtual WaterAndDrinksTags Tag { get; set; }
}