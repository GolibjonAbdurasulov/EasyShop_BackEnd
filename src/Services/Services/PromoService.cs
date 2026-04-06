using DatabaseBroker.Repositories.Categories.MainProductCategoryRepository;
using DatabaseBroker.Repositories.Products.FoodProductRepository;
using DatabaseBroker.Repositories.Products.HouseHoldProductsRepository;
using DatabaseBroker.Repositories.Products.OilProductsRepository;
using DatabaseBroker.Repositories.Products.WaterAndDrinksRepository;
using DatabaseBroker.Repositories.PromoRepositories;
using Entity.Attributes;
using Entity.Exceptions;
using Entity.Models.Promo;
using Services.Dtos.PromoDtos;
using Services.Interfaces;

namespace Services.Services;
[Injectable]
public class PromoService : IPromoService
{
    private IPromoRepository _promoRepository;
    private IFoodProductRepository  _foodProductRepository;
    private IOilProductsRepository   _oilProductsRepository;
    private IWaterAndDrinksRepository _waterAndDrinksRepository;
    private IHouseHoldProductsRepository _houseHoldProductsRepository;
    private IMainProductCategoryRepository  _mainProductCategoryRepository;

    public PromoService(IPromoRepository promoRepository, IFoodProductRepository foodProductRepository, IOilProductsRepository oilProductsRepository, IWaterAndDrinksRepository waterAndDrinksRepository, IHouseHoldProductsRepository houseHoldProductsRepository, IMainProductCategoryRepository mainProductCategoryRepository)
    {
        _promoRepository = promoRepository;
        _foodProductRepository = foodProductRepository;
        _oilProductsRepository = oilProductsRepository;
        _waterAndDrinksRepository = waterAndDrinksRepository;
        _houseHoldProductsRepository = houseHoldProductsRepository;
        _mainProductCategoryRepository = mainProductCategoryRepository;
    }

    public async Task<PromoGetDto> CreatePromoAsync(PromoCreationDto promoDto)
    {
        var promo = new Promo
        {
            ProductId = promoDto.ProductId,
            MainCategoryId = promoDto.MainCategoryId,
            NewPrice = promoDto.NewPrice,
            StartDate = promoDto.StartDate,
            EndDate = promoDto.EndDate,
        };
        var resPromo=await _promoRepository.AddAsync(promo);
        var resDto= await this.GeneratePromoGetDto(resPromo);

        return resDto;
    }

    public async Task<PromoGetDto> UpdatePromoAsync(PromoUpdateDto promoDto)
    {
        var promo = await _promoRepository.GetByIdAsync(promoDto.Id);
        if (promo == null)
            throw new NotFoundException("Promo not found on PromoService");
        
        promo.ProductId = promoDto.ProductId;
        promo.MainCategoryId = promoDto.MainCategoryId;
        promo.NewPrice=promoDto.NewPrice;
        promo.StartDate=promoDto.StartDate;
        promo.EndDate=promoDto.EndDate;
        var updatedPromo=await _promoRepository.UpdateAsync(promo);
        var resDto= await this.GeneratePromoGetDto(updatedPromo);
        return resDto;
    }



    public async Task<bool> DeletePromoAsync(long id)
    {
        var promo =await _promoRepository.GetByIdAsync(id);
        var res=await _promoRepository.RemoveAsync(promo);
        if (res==null)
            return false;
        return true;
    }

    public async Task<PromoGetDto> GetPromoByIdAsync(long id)
    {
        var promo=await _promoRepository.GetByIdAsync(id); 
        if (promo==null)
            throw new NotFoundException("Promo not found on PromoService");
        var promoDto= await this.GeneratePromoGetDto(promo);
        return promoDto;
    }

    public async Task<List<PromoGetDto>> GetAllPromoAsync()
    { 
        List<PromoGetDto> resPromos=  new List<PromoGetDto>();

        var promosModels =_promoRepository.GetAllAsQueryable().ToList();
        if (promosModels==null)
            throw new NotFoundException("Promos is null");

        foreach (var promoModel in promosModels)
        {
            var temp=await GeneratePromoGetDto(promoModel);
            resPromos.Add(temp);
        }
        return resPromos;
    }

    private async Task<PromoGetDto> GeneratePromoGetDto(Promo promo)
    {
        var mainCategory = await _mainProductCategoryRepository.GetByIdAsync(promo.MainCategoryId);

        var resDto = new PromoGetDto
        {
            Id = promo.Id,
            ProductId = promo.ProductId,
            MainCategoryId = promo.MainCategoryId,
            ProductCategoryId = promo.ProductId,
            NewPrice = promo.NewPrice,
            StartDate = promo.StartDate,
            EndDate = promo.EndDate
        };
        switch (mainCategory.MainCategoryName.uz)
        {
            case "Oziq-ovqat mahsulotlar":
                var foodProduct = await _foodProductRepository.GetByIdAsync(promo.ProductId);
                resDto.OldPrice = foodProduct.Price;
                resDto.ProductName = foodProduct.Name.uz;
                resDto.AboutProduct = foodProduct.About.uz;
                resDto.ProductImageId = foodProduct.ProductImageId;
                resDto.TagId = foodProduct.TagId;
                resDto.ProductCategoryId = foodProduct.FoodCategoryId;
                resDto.ProductImageId = foodProduct.ProductImageId;
                resDto.QuantityBoxes = foodProduct.WarehouseDates.QuantityBoxes;
                resDto.QuantityInOneBox = foodProduct.WarehouseDates.QuantityInOneBox;
                resDto.QuantityPieces = foodProduct.WarehouseDates.QuantityPieces;
                break;
            case "Ho'jalik mollari":
                var houseHoldProduct = await _houseHoldProductsRepository.GetByIdAsync(promo.ProductId);
                resDto.OldPrice = houseHoldProduct.Price;
                resDto.ProductName = houseHoldProduct.Name.uz;
                resDto.AboutProduct = houseHoldProduct.About.uz;
                resDto.ProductImageId = houseHoldProduct.ProductImageId;
                resDto.TagId = houseHoldProduct.TagId;
                resDto.ProductCategoryId = houseHoldProduct.HouseholdCategoryId;
                resDto.ProductImageId = houseHoldProduct.ProductImageId;
                resDto.QuantityBoxes = houseHoldProduct.WarehouseDates.QuantityBoxes;
                resDto.QuantityInOneBox = houseHoldProduct.WarehouseDates.QuantityInOneBox;
                resDto.QuantityPieces = houseHoldProduct.WarehouseDates.QuantityPieces;
                break;
            case "Yog' mahsulotlari":
                var oilProduct = await _oilProductsRepository.GetByIdAsync(promo.ProductId);
                resDto.OldPrice = oilProduct.Price;
                resDto.ProductName = oilProduct.Name.uz;
                resDto.AboutProduct = oilProduct.About.uz;
                resDto.ProductImageId = oilProduct.ProductImageId;
                resDto.TagId = oilProduct.TagId;
                resDto.ProductCategoryId = -1;
                resDto.ProductImageId = oilProduct.ProductImageId;
                resDto.QuantityBoxes = oilProduct.WarehouseDates.QuantityBoxes;
                resDto.QuantityInOneBox = oilProduct.WarehouseDates.QuantityInOneBox;
                resDto.QuantityPieces = oilProduct.WarehouseDates.QuantityPieces;
                break;
            case "Suv va gazli ichimliklar":
                var waterProduct = await _waterAndDrinksRepository.GetByIdAsync(promo.ProductId);
                resDto.OldPrice = waterProduct.Price;
                resDto.ProductName = waterProduct.Name.uz;
                resDto.AboutProduct = waterProduct.About.uz;
                resDto.ProductImageId = waterProduct.ProductImageId;
                resDto.TagId = waterProduct.TagId;
                resDto.ProductCategoryId = -1;
                resDto.ProductImageId = waterProduct.ProductImageId;
                resDto.QuantityBoxes = waterProduct.WarehouseDates.QuantityBoxes;
                resDto.QuantityInOneBox = waterProduct.WarehouseDates.QuantityInOneBox;
                resDto.QuantityPieces = waterProduct.WarehouseDates.QuantityPieces;
                break;
            default:
                throw new NotFoundException("Invalid product type on PromoService");
        }
        return resDto;
    }
}