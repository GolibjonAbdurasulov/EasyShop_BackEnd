using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Product.Categories;
using Entity.Models.Product.Tags;

namespace Entity.Models.Product.Products;
[Table("food_products")]
public class FoodProducts : Product
{
   [Column("category_id"),ForeignKey(nameof(FoodProductCategory))]public long CategoryId { get; set; }
   public virtual FoodProductCategory FoodProductCategory { get; set; }
   [Column("tag_id"),ForeignKey(nameof(Tag))]public long TagId { get; set; }
   public FoodProductTags Tag { get; set; }
}