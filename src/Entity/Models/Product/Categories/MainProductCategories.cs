using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;
using Entity.Models.File;

namespace Entity.Models.Product.Categories;
[Table("main_product_categories")]
public class MainProductCategories : ModelBase<long>
{
    [Column("category_name",TypeName = "jsonb")]public MultiLanguageField MainCategoryName { get; set; }
    [Column("file_id"),ForeignKey(nameof(CategoryImage))] public long MainCategoryImageId  { get; set; }
    public virtual FileModel CategoryImage { get; set; }
}