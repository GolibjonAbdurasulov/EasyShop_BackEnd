using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Product.Categories;
using Entity.Models.Product.Tags;

namespace Entity.Models.Product.Products;
[Table("food_products")]
public class FoodProducts : Product
{
   [Column("food_category_id"),ForeignKey(nameof(FoodProductCategory))]public long FoodCategoryId { get; set; }
   public virtual FoodProductCategory FoodProductCategory { get; set; }
   [Column("tag_id"),ForeignKey(nameof(Tag))]public long TagId { get; set; }
   public virtual FoodProductTags Tag { get; set; }
}