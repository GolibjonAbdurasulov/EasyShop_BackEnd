using System.Net;
using API.Common;
using API.Controllers.ProductsControllers.OilProductsController.Dtos;
using DatabaseBroker.Repositories.Products.OilProductsRepository;
using DatabaseBroker.Repositories.Tags.OilProductTagsRepository;
using DatabaseBroker.Repositories.WarehouseDatesRepositories;
using Entity.Models.Product;
using Entity.Models.Product.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.ProductsControllers.OilProductsController;

[ApiController]
[Route("[controller]/[action]")]
public class OilProductsController : ControllerBase
{
    private IOilProductsRepository HoldProductsRepository { get; set; }
    private IOilProductTagsRepository OilProduct { get; set; }
    private IWarehouseDatesRepository WarehouseDatesRepository { get; set; }

    public OilProductsController(IOilProductsRepository ourTeamRepository, IOilProductTagsRepository houseHoldProductTags, IWarehouseDatesRepository warehouseDatesRepository)
    {
        this.HoldProductsRepository = ourTeamRepository;
        OilProduct = houseHoldProductTags;
        WarehouseDatesRepository = warehouseDatesRepository;
    }

    [HttpPost]
    [Authorize]
    public async Task<ResponseModelBase> CreateAsync( OilProductCreationDto dto)
    {
        var warehouse = await WarehouseDatesRepository.AddAsync(new WarehouseDates
        {
            QuantityBoxes = 0,
            QuantityPieces = 0,
            QuantityInOneBox = 0
        });
        var entity = new OilProducts
        {
            Name = dto.Name,
            About = dto.About,
            Price = dto.Price,
            ProductImageId = dto.ImageId,
            MainCategoryId = dto.MainCategoryId,
            TagId = dto.TagId,
            WarehouseDatesId = warehouse.Id
        };
        var resEntity=await HoldProductsRepository.AddAsync(entity);
        
        var resDto = new OilProductGetDto
        {
            Id = resEntity.Id,
            Name = resEntity.Name,
            About = resEntity.About,
            Price = resEntity.Price,
            ImageId = resEntity.ProductImageId,
            MainCategoryId = resEntity.MainCategoryId,
            TagId = resEntity.TagId,
            WarehouseDatesId = resEntity.WarehouseDatesId,
            QuantityBoxes = resEntity.WarehouseDates.QuantityBoxes,
            QuantityPieces = resEntity.WarehouseDates.QuantityPieces, 
            QuantityInOneBox = resEntity.WarehouseDates.QuantityInOneBox
        };
        return new ResponseModelBase(resDto);
    }


  
    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateAsync( OilProductUpdateDto dto)
    {
        var res =  await HoldProductsRepository.GetByIdAsync(dto.Id);
        res.Name = dto.Name;
        res.About = dto.About; 
        res.Price = dto.Price;
        res.ProductImageId = dto.ImageId;
        res.MainCategoryId = dto.MainCategoryId;
        res.TagId = dto.TagId;
        res.WarehouseDatesId = dto.WarehouseDatesId;
        
        await HoldProductsRepository.UpdateAsync(res);
        return new ResponseModelBase(dto);
    }
    
    
    [HttpDelete]
    [Authorize]
    public async Task<ResponseModelBase> DeleteAsync(long id)
    {
        
        var res =  await HoldProductsRepository.GetByIdAsync(id);
        await HoldProductsRepository.RemoveAsync(res);
        return new ResponseModelBase(res);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetByIdAsync(long id)
    {
        var resEntity =  await HoldProductsRepository.GetByIdAsync(id);
        var dto = new OilProductGetDto
        { 
            Id = resEntity.Id,
            Name = resEntity.Name,
            About = resEntity.About,
            Price = resEntity.Price,
            ImageId = resEntity.ProductImageId,
            MainCategoryId = resEntity.MainCategoryId,
            TagId = resEntity.TagId,
            WarehouseDatesId = resEntity.WarehouseDatesId,
            QuantityBoxes = resEntity.WarehouseDates.QuantityBoxes,
            QuantityPieces = resEntity.WarehouseDates.QuantityPieces, 
            QuantityInOneBox = resEntity.WarehouseDates.QuantityInOneBox
        };
        return new ResponseModelBase(dto);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllAsync()
    {
        var res =   HoldProductsRepository.GetAllAsQueryable().ToList();
        List<OilProductGetDto> dtos = new List<OilProductGetDto>();
        foreach (OilProducts model in res)
        {
            dtos.Add(new OilProductGetDto
            {
                Id = model.Id,
                Name = model.Name,
                About = model.About,
                Price = model.Price,
                ImageId = model.ProductImageId,
                MainCategoryId = model.MainCategoryId,
                TagId = model.TagId,
                WarehouseDatesId = model.WarehouseDatesId,
                QuantityBoxes = model.WarehouseDates.QuantityBoxes,
                QuantityPieces = model.WarehouseDates.QuantityPieces, 
                QuantityInOneBox = model.WarehouseDates.QuantityInOneBox
            });
        }
        
        return new ResponseModelBase(dtos);
    }   
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllByTagsAsync(long tagId)
    {
        var res =   HoldProductsRepository.GetAllAsQueryable().Where(item=>item.TagId==tagId).ToList();
        List<OilProductGetDto> dtos = new List<OilProductGetDto>();
        foreach (OilProducts model in res)
        {
            dtos.Add(new OilProductGetDto
            {
                Id = model.Id,
                Name = model.Name,
                About = model.About,
                Price = model.Price,
                ImageId = model.ProductImageId,
                TagId = model.TagId,
                WarehouseDatesId = model.WarehouseDatesId,
                QuantityBoxes = model.WarehouseDates.QuantityBoxes,
                QuantityPieces = model.WarehouseDates.QuantityPieces, 
                QuantityInOneBox = model.WarehouseDates.QuantityInOneBox
            });
        }
        
        return new ResponseModelBase(dtos);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> SearchAsync(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return new ResponseModelBase("Query string cannot be empty", HttpStatusCode.NotFound);

        var res = HoldProductsRepository
            .GetAllAsQueryable()
            .Where(p =>
                p.Name.uz.ToLower().Contains(query.ToLower()))
            .ToList();

        var dtos = res.Select(model => new OilProductGetDto
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