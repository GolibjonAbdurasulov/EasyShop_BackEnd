using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using Entity.Models.Common;
using Entity.Models.File;

namespace Entity.Models.Product.Categories;
[Table("food_product_categories")]
public class FoodProductCategory : ModelBase<long>
{
    [Column("category_name",TypeName = "jsonb")]public MultiLanguageField CategoryName { get; set; }
    [Column("file_id"),ForeignKey(nameof(CategoryImage))] public long CategoryImageId  { get; set; }
    public virtual FileModel CategoryImage { get; set; }
}