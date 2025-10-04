using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using Entity.Models.Common;
using Entity.Models.File;

namespace Entity.Models.Product.Categories;
[Table("food_product_categories")]
public class FoodProductCategory : ModelBase<long>
{
    [Column("food_produc_category_name",TypeName = "jsonb")]public MultiLanguageField FoodProductCategoryName { get; set; }
    [Column("food_product_image_id"),ForeignKey(nameof(CategoryImage))] public Guid FoodProductCategoryImageId  { get; set; }
    public virtual FileModel CategoryImage { get; set; }
}