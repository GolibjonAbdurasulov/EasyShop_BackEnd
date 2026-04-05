using Entity.Models.Promo;
using Services.Dtos.PromoDtos;

namespace Services.Interfaces;

public interface IPromoService
{
    public Task<PromoGetDto> CreatePromoAsync(PromoCreationDto promo);
    public Task<PromoGetDto> UpdatePromoAsync(PromoUpdateDto promo);
    public Task<bool> DeletePromoAsync(long id);
    public Task<PromoGetDto> GetPromoByIdAsync(long id);
    public Task<List<PromoGetDto>> GetAllPromoAsync();
}