using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;
using Entity.Models.Users;

namespace Entity.Models.Order;

[Table("cart")]
public class Cart : ModelBase<long>
{
    [Column("products_id", TypeName = "jsonb")] public List<ProductItem> ProductsId { get; set; } = new();

    [Column("customer_id"), ForeignKey(nameof(Customer))]
    public long CustomerId { get; set; }

    public virtual User Customer { get; set; }
}

public class ProductItem
{
    public long  Id { get; set; }
    public string ProductType { get; set; }
    public long Quantity { get; set; }
}