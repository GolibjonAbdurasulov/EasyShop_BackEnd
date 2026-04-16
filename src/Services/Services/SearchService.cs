using DatabaseBroker.Repositories.Products.FoodProductRepository;
using DatabaseBroker.Repositories.Products.HouseHoldProductsRepository;
using DatabaseBroker.Repositories.Products.OilProductsRepository;
using DatabaseBroker.Repositories.Products.WaterAndDrinksRepository;
using DatabaseBroker.Repositories.Tags.FoodProductTagsRepository;
using DatabaseBroker.Repositories.Tags.HouseHoldProductTagsRepository;
using DatabaseBroker.Repositories.Tags.OilProductTagsRepository;
using DatabaseBroker.Repositories.Tags.WaterAndDrinkTagsRepository;
using Entity.Attributes;
using Entity.Exceptions;
using Entity.Models.Product;
using Entity.Models.Product.Products;
using Microsoft.AspNetCore.Mvc;
using Services.Dtos.SearchDtos;
using Services.Interfaces;

namespace Services.Services;
[Injectable]
public class SearchService : ISearchService
{
    private readonly IFoodProductRepository  _foodProductRepository;
    private readonly IWaterAndDrinksRepository  _waterAndDrinksRepository;
    private readonly IOilProductsRepository   _oilProductsRepository;
    private readonly IHouseHoldProductsRepository   _holdProductsRepository;
    private readonly IFoodProductTagsRepository  _foodProductTagsRepository;
    private readonly IHouseHoldProductTagsRepository  _houseHoldProductTagsRepository;
    private readonly IWaterAndDrinkTagsRepository  _waterAndDrinkTagsRepository;
    private readonly IOilProductTagsRepository  _oilProductTagsRepository;

    public SearchService(IWaterAndDrinkTagsRepository waterAndDrinkTagsRepository, IFoodProductRepository foodProductRepository, IWaterAndDrinksRepository waterAndDrinksRepository, IOilProductsRepository oilProductsRepository, IHouseHoldProductsRepository holdProductsRepository, IFoodProductTagsRepository foodProductTagsRepository, IHouseHoldProductTagsRepository houseHoldProductTagsRepository, IOilProductTagsRepository oilProductTagsRepository)
    {
        _waterAndDrinkTagsRepository = waterAndDrinkTagsRepository;
        _foodProductRepository = foodProductRepository;
        _waterAndDrinksRepository = waterAndDrinksRepository;
        _oilProductsRepository = oilProductsRepository;
        _holdProductsRepository = holdProductsRepository;
        _foodProductTagsRepository = foodProductTagsRepository;
        _houseHoldProductTagsRepository = houseHoldProductTagsRepository;
        _oilProductTagsRepository = oilProductTagsRepository;
    }


    public async Task<List<SearchResponse>> Search(string q, int page = 1, int pageSize = 20)
    {
        string query = q.ToLower();
        var foodProducts = _foodProductRepository.GetAllAsQueryable()
            .Where(f => f.Name.uz.ToLower().Contains(query) || 
                        f.Tag.TagName.uz.ToLower().Contains(query))
            .Select(f => new SearchResponse
            {
                ProductId = f.Id,
                ProductName = f.Name.uz,
                AboutProduct = f.About.uz,
                MainCategoryId = f.MainCategoryId,
                ProductCategoryId = f.FoodCategoryId,
                TagId = f.TagId,
                Price = f.Price,
                ProductImageId = f.ProductImageId,
                QuantityBoxes = f.WarehouseDates.QuantityBoxes,
                QuantityInOneBox = f.WarehouseDates.QuantityInOneBox
            });
        
        var houseHolsProducts = _holdProductsRepository.GetAllAsQueryable()
            .Where(f => f.Name.uz.ToLower().Contains(query) || 
                        f.Tag.TagName.uz.ToLower().Contains(query))
            .Select(f => new SearchResponse
            {
                ProductId = f.Id,
                ProductName = f.Name.uz,
                AboutProduct = f.About.uz,
                MainCategoryId = f.MainCategoryId,
                ProductCategoryId = f.HouseholdCategoryId,
                TagId = f.TagId,
                Price = f.Price,
                ProductImageId = f.ProductImageId,
                QuantityBoxes = f.WarehouseDates.QuantityBoxes,
                QuantityInOneBox = f.WarehouseDates.QuantityInOneBox
            });
        
         var oilProducts = _oilProductsRepository.GetAllAsQueryable()
             .Where(f => f.Name.uz.ToLower().Contains(query) || 
                         f.Tag.TagName.uz.ToLower().Contains(query))
             .Select(f => new SearchResponse
             {
                 ProductId = f.Id,
                 ProductName = f.Name.uz,
                 AboutProduct = f.About.uz,
                 MainCategoryId = f.MainCategoryId,
                 TagId = f.TagId,
                 Price = f.Price,
                 ProductImageId = f.ProductImageId,
                 QuantityBoxes = f.WarehouseDates.QuantityBoxes,
                 QuantityInOneBox = f.WarehouseDates.QuantityInOneBox
             });
        
         var waterProducts = _waterAndDrinksRepository.GetAllAsQueryable()
             .Where(f => f.Name.uz.ToLower().Contains(query) || 
                         f.Tag.TagName.uz.ToLower().Contains(query))
             .Select(f => new SearchResponse
             {
                 ProductId = f.Id,
                 ProductName = f.Name.uz,
                 AboutProduct = f.About.uz,
                 MainCategoryId = f.MainCategoryId,
                 TagId = f.TagId,
                 Price = f.Price,
                 ProductImageId = f.ProductImageId,
                 QuantityBoxes = f.WarehouseDates.QuantityBoxes,
                 QuantityInOneBox = f.WarehouseDates.QuantityInOneBox
             });
        
        List<SearchResponse> res = new List<SearchResponse>();
        res.AddRange(foodProducts);
        res.AddRange(houseHolsProducts);
        res.AddRange(oilProducts);
        res.AddRange(waterProducts);

        return res
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        
    }

    private async Task<object> GetProduct(string categoryName,long productId)
    {
        switch (categoryName)
        {
            case "Oziq-ovqat mahsulotlar":
                var foodProduct = await  _foodProductRepository.GetByIdAsync(productId);
                return foodProduct;
                //resDto.QuantityPieces = foodProduct.WarehouseDates.QuantityPieces;
                break;
            case "Ho'jalik mollari":
                var houseHoldProduct = await _holdProductsRepository.GetByIdAsync(productId);
                return houseHoldProduct;
                break;
            case "Yog' mahsulotlari":
                var oilProduct = await _oilProductsRepository.GetByIdAsync(productId);
                return oilProduct;
                break;
            case "Suv va gazli ichimliklar":
                var waterProduct = await _waterAndDrinksRepository.GetByIdAsync(productId);
                return waterProduct;
                break;
            default:
                throw new NotFoundException("Invalid product type on PromoService");
        }
    }
}