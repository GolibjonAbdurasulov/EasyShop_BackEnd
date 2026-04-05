namespace Services.Dtos.PromoDtos;

public class PromoGetDto
{
    public  long Id { get; set; }
    public  long ProductId { get; set; }
    public  string ProductName { get; set; }
    public  string AboutProduct { get; set; }
    public long MainCategoryId { get; set; } = -1;
    public long ProductCategoryId { get; set; } = -1;
    public long TagId { get; set; } = -1;
    public decimal OldPrice { get; set; } = 0;
    public decimal NewPrice { get; set; } = 0;
    public Guid ProductImageId { get; set; }
    public string ImageLink { get => $"https://back.easyshop.uz/File/DownloadFile/download/{ProductImageId}"; }
    public int QuantityBoxes { get; set; }
    public int QuantityPieces { get; set; }
    public int QuantityInOneBox { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}