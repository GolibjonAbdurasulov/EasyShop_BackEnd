using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Product.Categories;
using Entity.Models.Product.Tags;

namespace Entity.Models.Product.Products;
[Table("household_products")]
public class HouseholdProducts : Product
{
    [Column("hose_hold_category_id"),ForeignKey(nameof(HouseholdProductCategory))]public long HouseholdCategoryId { get; set; }
    public virtual HouseholdProductCategory HouseholdProductCategory { get; set; }
    [Column("tag_id"),ForeignKey(nameof(Tag))]public long TagId { get; set; }
    public HouseholdProductTags Tag { get; set; }
}