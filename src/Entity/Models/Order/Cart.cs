using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;
using Entity.Models.Users;

namespace Entity.Models.Order;
[Table("cart")]
public class Cart : ModelBase<long>
{
    [Column("products_id")]public List<long> ProductsId { get; set; }
    [Column("customer_id"),ForeignKey(nameof(Customer))]public long CustomerId { get; set; }
    public virtual User Customer { get; set; }
}