using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;
using Entity.Models.File;
using Entity.Models.Product.Tags;

namespace Entity.Models.Product;
public class Product : ModelBase<long>
{
    [Column("name",TypeName = "jsonb")]public MultiLanguageField Name { get; set; }
    [Column("about",TypeName = "jsonb")]public MultiLanguageField About { get; set; }
    [Column("price")]public decimal Price { get; set; }
    [Column("image_id"),ForeignKey(nameof(Image))]public long ImageId { get; set; }
    public virtual FileModel Image { get; set; }
    
    [Column("image_id"),ForeignKey(nameof(CategoryImage))]public long CategoryImageId { get; set; }
    public virtual FileModel CategoryImage { get; set; }
    
}