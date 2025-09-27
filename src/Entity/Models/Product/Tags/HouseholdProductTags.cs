using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;

namespace Entity.Models.Product.Tags;
[Table("household_product_tags")]
public class HouseholdProductTags :  ModelBase<long>
{
    [Column("tag_name",TypeName = "jsonb")]public MultiLanguageField TagName { get; set; }

}