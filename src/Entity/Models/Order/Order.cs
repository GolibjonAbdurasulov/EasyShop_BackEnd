using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Entity.Enums;
using Entity.Models.Common;
using Entity.Models.Users;

namespace Entity.Models.Order;
[Table("orders")]
public class Order : ModelBase<long>
{
    [Column("products_id", TypeName = "jsonb")] public List<ProductItem> ProductsIds { get; set; } = new();
    [Column("total_price")]public decimal TotalPrice { get; set; }
    [Column("order_status")]public OrderStatus OrderStatus { get; set; }
    [Column("delivery_date")]public DateTime DeliveryDate { get; set; }
    [Column("customer_id"),ForeignKey(nameof(Client))]public long CustomerId { get; set; }
    public virtual Client.Client Client { get; set; }
}