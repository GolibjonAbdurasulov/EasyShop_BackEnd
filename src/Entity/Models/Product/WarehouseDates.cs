
using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;

namespace Entity.Models.Product;
[Table("WarehouseDates")]
public class WarehouseDates : ModelBase<long>
{
    [Column("quantity_boxes")]public int QuantityBoxes { get; set; }
    [Column("quantity_pieces")] public int QuantityPieces { get; set; }
    [Column("quantity_ine_one_box")] public int QuantityInOneBox { get; set; }
}