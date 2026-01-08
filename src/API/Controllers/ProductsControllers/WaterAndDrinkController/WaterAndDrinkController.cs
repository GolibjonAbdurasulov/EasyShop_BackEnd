using System.Net;
using API.Common;
using API.Controllers.ProductsControllers.WaterAndDrinkController.Dtos;
using DatabaseBroker.Repositories.Products.WaterAndDrinksRepository;
using DatabaseBroker.Repositories.Tags.WaterAndDrinkTagsRepository;
using DatabaseBroker.Repositories.WarehouseDatesRepositories;
using Entity.Models.Product;
using Entity.Models.Product.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.ProductsControllers.WaterAndDrinkController;
[ApiController]
[Route("[controller]/[action]")]
public class WaterAndDrinkController : ControllerBase
{
     private IWaterAndDrinksRepository WaterAndDrinksRepository { get; set; }
    private IWaterAndDrinkTagsRepository WaterAndDrinkTagsRepository { get; set; }
    private IWarehouseDatesRepository WarehouseDatesRepository { get; set; }

    public WaterAndDrinkController(IWaterAndDrinksRepository ourTeamRepository, IWaterAndDrinkTagsRepository houseHoldProductTags, IWarehouseDatesRepository warehouseDatesRepository)
    {
        this.WaterAndDrinksRepository = ourTeamRepository;
        WaterAndDrinkTagsRepository = houseHoldProductTags;
        WarehouseDatesRepository = warehouseDatesRepository;
    }

    [HttpPost]
    [Authorize]
    public async Task<ResponseModelBase> CreateAsync( WaterAndDrinkProductCreationDto dto)
    {
        var warehouse = await WarehouseDatesRepository.AddAsync(new WarehouseDates
        {
            QuantityBoxes = 0,
            QuantityPieces = 0,
            QuantityInOneBox = 0
        });
        var entity = new WaterAndDrinks()
        {
            Name = dto.Name,
            About = dto.About,
            Price = dto.Price,
            ProductImageId = dto.ImageId,
            MainCategoryId = dto.MainCategoryId,
            TagId = dto.TagId,
            WarehouseDatesId = warehouse.Id,
        };
        var resEntity=await WaterAndDrinksRepository.AddAsync(entity);
        
        var resDto = new WaterAndDrinkProductGetDto
        {
            Id = resEntity.Id,
            Name = resEntity.Name,
            About = resEntity.About,
            Price = resEntity.Price,
            ImageId = resEntity.ProductImageId,
            MainCategoryId = resEntity.MainCategoryId,
            TagId = resEntity.TagId,
            WarehouseDatesId = resEntity.WarehouseDatesId
        };
        return new ResponseModelBase(resDto);
    }


  
    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateAsync( WaterAndDrinkProductUpdateDto dto)
    {
        var res =  await WaterAndDrinksRepository.GetByIdAsync(dto.Id);
        res.Name = dto.Name;
        res.About = dto.About; 
        res.Price = dto.Price;
        res.ProductImageId = dto.ImageId;
        res.MainCategoryId = dto.MainCategoryId;
        res.TagId = dto.TagId;
        res.WarehouseDatesId = dto.WarehouseDatesId;
        
        await WaterAndDrinksRepository.UpdateAsync(res);
        return new ResponseModelBase(dto);
    }
    
    
    [HttpDelete]
    [Authorize]
    public async Task<ResponseModelBase> DeleteAsync(long id)
    {
        
        var res =  await WaterAndDrinksRepository.GetByIdAsync(id);
        await WaterAndDrinksRepository.RemoveAsync(res);
        return new ResponseModelBase(res);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetByIdAsync(long id)
    {
        var resEntity =  await WaterAndDrinksRepository.GetByIdAsync(id);
        var dto = new WaterAndDrinkProductGetDto
        { 
            Id = resEntity.Id,
            Name = resEntity.Name,
            About = resEntity.About,
            Price = resEntity.Price,
            ImageId = resEntity.ProductImageId,
            MainCategoryId = resEntity.MainCategoryId,
            TagId = resEntity.TagId,
            WarehouseDatesId = resEntity.WarehouseDatesId
        };
        return new ResponseModelBase(dto);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllAsync()
    {
        var res =   WaterAndDrinksRepository.GetAllAsQueryable().ToList();
        List<WaterAndDrinkProductGetDto> dtos = new List<WaterAndDrinkProductGetDto>();
        foreach (WaterAndDrinks model in res)
        {
            dtos.Add(new WaterAndDrinkProductGetDto
            {
                Id = model.Id,
                Name = model.Name,
                About = model.About,
                Price = model.Price,
                ImageId = model.ProductImageId,
                TagId = model.TagId,
                WarehouseDatesId = model.WarehouseDatesId
            });
        }
        
        return new ResponseModelBase(dtos);
    }   
    
    
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllByTagsAsync(long tagId)
    {
        var res =   WaterAndDrinksRepository.GetAllAsQueryable().Where(item=>item.TagId==tagId).ToList();
        List<WaterAndDrinkProductGetDto> dtos = new List<WaterAndDrinkProductGetDto>();
        foreach (WaterAndDrinks model in res)
        {
            dtos.Add(new WaterAndDrinkProductGetDto
            {
                Id = model.Id,
                Name = model.Name,
                About = model.About,
                Price = model.Price,
                ImageId = model.ProductImageId,
                TagId = model.TagId,
                WarehouseDatesId = model.WarehouseDatesId
                
            });
        }
        
        return new ResponseModelBase(dtos);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> SearchAsync(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return new ResponseModelBase("Query string cannot be empty", HttpStatusCode.NotFound);

        var res = WaterAndDrinksRepository
            .GetAllAsQueryable()
            .Where(p =>
                p.Name.uz.ToLower().Contains(query.ToLower()))
            .ToList();

        var dtos = res.Select(model => new WaterAndDrinkProductGetDto
        {
            Id = model.Id,
            Name = model.Name,
            About = model.About,
            Price = model.Price,
            ImageId = model.ProductImageId,
            MainCategoryId = model.MainCategoryId,
            TagId = model.TagId,
            WarehouseDatesId = model.WarehouseDatesId
        }).ToList();

        if (dtos.Count==0) 
            return new ResponseModelBase("Query string cannot be empty", HttpStatusCode.NotFound);

        return new ResponseModelBase(dtos);
    }

}