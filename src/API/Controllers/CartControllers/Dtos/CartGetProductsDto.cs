namespace API.Controllers.CartControllers.Dtos;

public class CartGetProductsDto
{
    public long ProductId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int QuantityBox { get; set; }
    public int QuantityPiece { get; set; }
    public Guid ImageId { get; set; }
    public string ProductType { get; set; }
    
    public string ImageLink { get => $"https://back.easyshop.uz/File/DownloadFile/download/{ImageId}"; }
    
}