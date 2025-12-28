using System.Net;
using API.Common;
using API.Controllers.ProductsControllers.HouseholdProductsController.Dtos;
using DatabaseBroker.Repositories.Products.HouseHoldProductsRepository;
using DatabaseBroker.Repositories.Tags.HouseHoldProductTagsRepository;
using Entity.Models.Product;
using Entity.Models.Product.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.ProductsControllers.HouseholdProductsController;
[ApiController]
[Route("[controller]/[action]")]
public class HouseholdProductsController : ControllerBase
{
    private IHouseHoldProductsRepository HoldProductsRepository { get; set; }
    private IHouseHoldProductTagsRepository HouseholdProductTagsRepository { get; set; }
    public HouseholdProductsController(IHouseHoldProductsRepository ourTeamRepository, IHouseHoldProductTagsRepository houseHoldProductTags, IHouseHoldProductTagsRepository householdProductTagsRepository)
    {
        this.HoldProductsRepository = ourTeamRepository;
        HouseholdProductTagsRepository = householdProductTagsRepository;
    }

    [HttpPost]
    [Authorize]
    public async Task<ResponseModelBase> CreateAsync( HouseholdCreationDto dto)
    {
        var entity = new HouseholdProducts
        {
            Name = dto.Name,
            About = dto.About,
            Price = dto.Price,
            ProductImageId = dto.ImageId,
            MainCategoryId = dto.MainCategoryId,
            HouseholdCategoryId = dto.HouseholdCategoryId,
            TagId = dto.TagId,
            WarehouseDates = new WarehouseDates
            {
                QuantityBoxes = dto.QuantityBoxes,
                QuantityPieces = dto.QuantityPieces,
                QuantityInOneBox = dto.QuantityInOneBox,
            }
        };
        var resEntity=await HoldProductsRepository.AddAsync(entity);
        
        var resDto = new HouseholdGetDto
        {
            Id = resEntity.Id,
            Name = resEntity.Name,
            About = resEntity.About,
            Price = resEntity.Price,
            ImageId = resEntity.ProductImageId,
            MainCategoryId = resEntity.MainCategoryId,
            MainCategory = resEntity.MainCategory,
            HouseholdProductCategory = resEntity.HouseholdProductCategory,
            HouseholdProductCategoryId = resEntity.HouseholdCategoryId,
            TagId = resEntity.TagId,
            Tag = resEntity.Tag,
            QuantityBoxes = resEntity.WarehouseDates.QuantityBoxes,
            QuantityPieces = resEntity.WarehouseDates.QuantityPieces,
            QuantityInOneBox = resEntity.WarehouseDates.QuantityInOneBox
        };
        return new ResponseModelBase(resDto);
    }


  
    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateAsync( HouseholdUpdateDto dto)
    {
        var res =  await HoldProductsRepository.GetByIdAsync(dto.Id);
        res.Name = dto.Name;
        res.About = dto.About; 
        res.Price = dto.Price;
        res.ProductImageId = dto.ImageId;
        res.MainCategoryId = dto.MainCategoryId;
        res.HouseholdCategoryId = dto.HouseholdProductCategoryId;
        res.TagId = dto.TagId;
        res.WarehouseDates.QuantityBoxes = dto.QuantityBoxes;
        res.WarehouseDates.QuantityPieces = dto.QuantityPieces;
        res.WarehouseDates.QuantityInOneBox = dto.QuantityInOneBox;
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
        var dto = new HouseholdGetDto
        {
            Id = resEntity.Id,
            Name = resEntity.Name,
            About = resEntity.About,
            Price = resEntity.Price,
            ImageId = resEntity.ProductImageId,
            MainCategoryId = resEntity.MainCategoryId,
            MainCategory = resEntity.MainCategory,
            HouseholdProductCategory = resEntity.HouseholdProductCategory,
            HouseholdProductCategoryId = resEntity.HouseholdCategoryId,
            TagId = resEntity.TagId,
            Tag = resEntity.Tag,
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
        List<HouseholdGetDto> dtos = new List<HouseholdGetDto>();
        foreach (HouseholdProducts resEntity in res)
        {
            dtos.Add(new HouseholdGetDto
            {
                Id = resEntity.Id,
                Name = resEntity.Name,
                About = resEntity.About,
                Price = resEntity.Price,
                ImageId = resEntity.ProductImageId,
                MainCategoryId = resEntity.MainCategoryId,
                MainCategory = resEntity.MainCategory,
                HouseholdProductCategory = resEntity.HouseholdProductCategory,
                HouseholdProductCategoryId = resEntity.HouseholdCategoryId,
                TagId = resEntity.TagId,
                Tag = resEntity.Tag,
                QuantityBoxes = resEntity.WarehouseDates.QuantityBoxes,
                QuantityPieces = resEntity.WarehouseDates.QuantityPieces,
                QuantityInOneBox = resEntity.WarehouseDates.QuantityInOneBox
            });
        }
        
        return new ResponseModelBase(dtos);
    }   
    
    
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllByTagsAsync(long tagId,long categoryId)
    {
        var res =   HoldProductsRepository.GetAllAsQueryable().Where(item
            =>item.TagId==tagId && 
              item.HouseholdCategoryId==categoryId).ToList();
        
        List<HouseholdGetDto> dtos = new List<HouseholdGetDto>();
        foreach (HouseholdProducts resEntity in res)
        {
            dtos.Add(new HouseholdGetDto
            {
                Id = resEntity.Id,
                Name = resEntity.Name,
                About = resEntity.About,
                Price = resEntity.Price,
                ImageId = resEntity.ProductImageId,
                MainCategoryId = resEntity.MainCategoryId,
                MainCategory = resEntity.MainCategory,
                HouseholdProductCategory = resEntity.HouseholdProductCategory,
                HouseholdProductCategoryId = resEntity.HouseholdCategoryId,
                TagId = resEntity.TagId,
                Tag = resEntity.Tag,
                QuantityBoxes = resEntity.WarehouseDates.QuantityBoxes,
                QuantityPieces = resEntity.WarehouseDates.QuantityPieces,
                QuantityInOneBox = resEntity.WarehouseDates.QuantityInOneBox
            });
        }
        
        return new ResponseModelBase(dtos);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetByCategoryAsync(long categoryId)
    {
        var resList =   HoldProductsRepository.GetAllAsQueryable().
            Where(item=>item.HouseholdCategoryId==categoryId).ToList();
        
        List<HouseholdProductCategoryGetDto> dtos = new List<HouseholdProductCategoryGetDto>();
        foreach (HouseholdProducts res in resList)
        {
            dtos.Add(new HouseholdProductCategoryGetDto
            {
                Id = res.Id,
                Name = res.Name,
                About = res.About,
                Price = res.Price,
                ImageId = res.ProductImageId,
                MainCategoryId = res.MainCategoryId,
                HouseholdProductCategoryId = res.HouseholdCategoryId,
                TagId = res.TagId,
                QuantityBoxes = res.WarehouseDates.QuantityBoxes,
                QuantityPieces = res.WarehouseDates.QuantityPieces,
                QuantityInOneBox = res.WarehouseDates.QuantityInOneBox
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
                p.Name.uz.ToLower().Contains(query.ToLower())
            )
            .ToList();

        var dtos = res.Select(model => new HouseholdGetDto()
        {
            Id = model.Id,
            Name = model.Name,
            About = model.About,
            Price = model.Price,
            ImageId = model.ProductImageId,
            MainCategoryId = model.MainCategoryId,
            MainCategory = model.MainCategory,
            TagId = model.TagId,
            Tag = model.Tag,
            QuantityBoxes = model.WarehouseDates.QuantityBoxes,
            QuantityPieces = model.WarehouseDates.QuantityPieces,
            QuantityInOneBox = model.WarehouseDates.QuantityInOneBox
        }).ToList();

        if (dtos.Count==0) 
            return new ResponseModelBase("Query string cannot be empty", HttpStatusCode.NotFound);

        return new ResponseModelBase(dtos);
    }

}