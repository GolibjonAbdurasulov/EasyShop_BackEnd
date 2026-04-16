namespace Services.Dtos.SearchDtos;

public class SearchResponse
{
    public  long ProductId { get; set; }
    public  string ProductName { get; set; }=string.Empty;
    public  string AboutProduct { get; set; }=string.Empty;
    public long MainCategoryId { get; set; } 
    public long? ProductCategoryId { get; set; } = -1;
    public long TagId { get; set; } = -1;
    public decimal Price { get; set; } = 0;
    public Guid ProductImageId { get; set; }
    public string ImageLink { get => $"https://back.easyshop.uz/File/DownloadFile/download/{ProductImageId}"; }
    public int QuantityBoxes { get; set; }
    //public int QuantityPieces { get; set; }
    public int QuantityInOneBox { get; set; }
}