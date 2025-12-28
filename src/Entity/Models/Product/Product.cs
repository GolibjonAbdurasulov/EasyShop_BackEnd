using System;
using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;
using Entity.Models.File;
using Entity.Models.Product.Categories;
using Entity.Models.Product.Tags;

namespace Entity.Models.Product;
public class Product : ModelBase<long>
{
    [Column("name",TypeName = "jsonb")]public MultiLanguageField Name { get; set; }
    [Column("about",TypeName = "jsonb")]public MultiLanguageField About { get; set; }
    [Column("price")]public decimal Price { get; set; }
    [Column("product_image_id"),ForeignKey(nameof(Image))]public Guid ProductImageId { get; set; }
    public virtual FileModel Image { get; set; }
    
    [Column("main_category_id"),ForeignKey(nameof(MainCategory))]public long MainCategoryId { get; set; }
    public virtual MainProductCategories MainCategory { get; set; }

    [Column("warehouse_dates_id")]public long WarehouseDatesId { get; set; }
    [Column("warehouse_dates")]public virtual WarehouseDates WarehouseDates { get; set; }

    [NotMapped]
    public int QuantityBox { get; set; }
    [NotMapped]
    public int QuantityPiece { get; set; }

    
}