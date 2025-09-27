using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;

namespace Entity.Models.Product.Tags;
[Table("water_and_drinks_tags")]
public class WaterAndDrinksTags :  ModelBase<long>
{
    [Column("tag_name",TypeName = "jsonb")]public MultiLanguageField TagName { get; set; }
}