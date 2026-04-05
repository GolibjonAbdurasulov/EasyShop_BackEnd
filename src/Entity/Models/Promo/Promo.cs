using System;
using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;

namespace Entity.Models.Promo;
[Table("promos")]
public class Promo : ModelBase<long>
{
   [Column("product_id")]public  long ProductId { get; set; }
   [Column("main_category_id")]public long MainCategoryId { get; set; } = -1; 
   [Column("new_price")]public decimal NewPrice { get; set; } = 0;
   [Column("start_date")]public DateTime StartDate { get; set; }
   [Column("end_date")]public DateTime EndDate { get; set; }
}