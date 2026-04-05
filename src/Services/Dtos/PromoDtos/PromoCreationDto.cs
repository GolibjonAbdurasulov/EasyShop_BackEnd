namespace Services.Dtos.PromoDtos;

public class PromoCreationDto
{
    public  long ProductId { get; set; }
    public long MainCategoryId { get; set; } = -1; 
    public decimal NewPrice { get; set; } = 0;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}