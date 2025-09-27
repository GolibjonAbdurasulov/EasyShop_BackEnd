using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;

namespace Entity.Models.Product.Tags;
[Table("food_product_tags")]
public class FoodProductTags : ModelBase<long>
{
    [Column("tag_name",TypeName = "jsonb")]public MultiLanguageField TagName { get; set; }
}