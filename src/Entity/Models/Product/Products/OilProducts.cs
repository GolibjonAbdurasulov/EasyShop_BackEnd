using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Product.Tags;

namespace Entity.Models.Product.Products;
[Table("oil_products")]
public class OilProducts : Product
{
    [Column("tag_id"),ForeignKey(nameof(Tag))]public long TagId { get; set; }
    public virtual OilProductTags Tag { get; set; }
}