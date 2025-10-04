using System;
using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;
using Entity.Models.File;

namespace Entity.Models.Product.Categories;
[Table("household_product_categories")]
public class HouseholdProductCategory : ModelBase<long>
{
    [Column("house_hold_category_name",TypeName = "jsonb")]public MultiLanguageField HouseHoldProductCategoryName { get; set; }
    [Column("house_hold_category_image_id"),ForeignKey(nameof(CategoryImage))] public Guid HouseHoldCategoryImageId  { get; set; }
    public virtual FileModel CategoryImage { get; set; }
}

