using System.Net;
using API.Common;
using API.Controllers.ProductsControllers.FoodProductController.Dtos;
using DatabaseBroker.Repositories.Products.FoodProductRepository;
using DatabaseBroker.Repositories.Tags.FoodProductTagsRepository;
using Entity.Models.Product;
using Entity.Models.Product.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.ProductsControllers.FoodProductController;
[ApiController]
[Route("[controller]/[action]")]
public class FoodProductsController : ControllerBase
{
    private IFoodProductTagsRepository FoodProductTagsRepository { get; set; }
    private IFoodProductRepository FoodProductRepository { get; set; }

    public FoodProductsController(IFoodProductRepository ourTeamRepository, IFoodProductTagsRepository foodProductTagsRepository)
    {
        this.FoodProductRepository = ourTeamRepository;
        FoodProductTagsRepository = foodProductTagsRepository;
    }

    [HttpPost]
    [Authorize]
    public async Task<ResponseModelBase> CreateAsync( FoodProductCreationDto dto)
    {
        var entity = new FoodProducts
        {
            Name = dto.Name,
            About = dto.About,
            Price = dto.Price,
            ProductImageId = dto.ImageId,
            MainCategoryId = dto.MainCategoryId,
            FoodCategoryId = dto.FoodProductCategoryId,
            TagId = dto.TagId,
            WarehouseDates = new WarehouseDates
            {
                QuantityBoxes = dto.QuantityBoxes,
                QuantityPieces = dto.QuantityPieces,
                QuantityInOneBox = dto.QuantityInOneBox,
            }
        };
        var resEntity=await FoodProductRepository.AddAsync(entity);
        
        var resDto = new FoodProductGetDto
        {
            Id = resEntity.Id,
            Name = resEntity.Name,
            About = resEntity.About,
            Price = resEntity.Price,
            ImageId = resEntity.ProductImageId,
            MainCategoryId = resEntity.MainCategoryId,
            FoodProductCategoryId = resEntity.FoodCategoryId,
            FoodProductCategory = resEntity.FoodProductCategory,
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
    public async Task<ResponseModelBase> UpdateAsync( FoodProductUpdateDto dto)
    {
        var res =  await FoodProductRepository.GetByIdAsync(dto.Id);
        res.Name = dto.Name;
        res.About = dto.About; 
        res.Price = dto.Price;
        res.ProductImageId = dto.ImageId;
        res.MainCategoryId = dto.MainCategoryId;
        res.FoodCategoryId = dto.FoodProductCategoryId;
        res.TagId = dto.TagId;
        res.WarehouseDates.QuantityBoxes = dto.QuantityBoxes;
        res.WarehouseDates.QuantityPieces = dto.QuantityPieces;
        res.WarehouseDates.QuantityInOneBox = dto.QuantityInOneBox;
        
        await FoodProductRepository.UpdateAsync(res);
        return new ResponseModelBase(dto);
    }
    
    
    [HttpDelete]
    [Authorize]
    public async Task<ResponseModelBase> DeleteAsync(long id)
    {
        
        var res =  await FoodProductRepository.GetByIdAsync(id);
        await FoodProductRepository.RemoveAsync(res);
        return new ResponseModelBase(res);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetByIdAsync(long id)
    {
        var res =  await FoodProductRepository.GetByIdAsync(id);
        var dto = new FoodProductGetDto
        {
            Id = res.Id,
            Name = res.Name,
            About = res.About,
            Price = res.Price,
            ImageId = res.ProductImageId,
            MainCategoryId = res.MainCategoryId,
            MainCategory = res.MainCategory,
            FoodProductCategoryId = res.FoodCategoryId,
            FoodProductCategory = res.FoodProductCategory,
            TagId = res.TagId,
            Tag = res.Tag,
            QuantityBoxes = res.WarehouseDates.QuantityBoxes,
            QuantityPieces = res.WarehouseDates.QuantityPieces,
            QuantityInOneBox = res.WarehouseDates.QuantityInOneBox
        };
        return new ResponseModelBase(dto);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllAsync()
    {
        var resList =   FoodProductRepository.GetAllAsQueryable().ToList();
        List<FoodProductGetDto> dtos = new List<FoodProductGetDto>();
        foreach (FoodProducts res in resList)
        {
            dtos.Add(new FoodProductGetDto
            {
                Id = res.Id,
                Name = res.Name,
                About = res.About,
                Price = res.Price,
                ImageId = res.ProductImageId,
                MainCategoryId = res.MainCategoryId,
                MainCategory = res.MainCategory,
                FoodProductCategoryId = res.FoodCategoryId,
                FoodProductCategory = res.FoodProductCategory,
                TagId = res.TagId,
                Tag = res.Tag,
                QuantityBoxes = res.WarehouseDates.QuantityBoxes,
                QuantityPieces = res.WarehouseDates.QuantityPieces,
                QuantityInOneBox = res.WarehouseDates.QuantityInOneBox
            });
        }
        
        return new ResponseModelBase(dtos);
    }   
    
    
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllByTagsAsync(long tagId,long categoryId)
    {
        var resList =   FoodProductRepository.GetAllAsQueryable().Where(item=>item.TagId==tagId&&categoryId==item.FoodCategoryId).ToList();
        List<FoodProductGetDto> dtos = new List<FoodProductGetDto>();
        foreach (FoodProducts res in resList)
        {
            dtos.Add(new FoodProductGetDto
            {
                Id = res.Id,
                Name = res.Name,
                About = res.About,
                Price = res.Price,
                ImageId = res.ProductImageId,
                MainCategoryId = res.MainCategoryId,
                MainCategory = res.MainCategory,
                FoodProductCategoryId = res.FoodCategoryId,
                FoodProductCategory = res.FoodProductCategory,
                TagId = res.TagId,
                Tag = res.Tag,
                QuantityBoxes = res.WarehouseDates.QuantityBoxes,
                QuantityPieces = res.WarehouseDates.QuantityPieces,
                QuantityInOneBox = res.WarehouseDates.QuantityInOneBox
            });
        }
        
        return new ResponseModelBase(dtos);
    }


    [HttpGet]
    public async Task<ResponseModelBase> GetByCategoryAsync(long categoryId)
    {
        var resList =   FoodProductRepository.GetAllAsQueryable().
            Where(item=>item.FoodCategoryId==categoryId).ToList();
        
        List<FoodProductCategoryGetDto> dtos = new List<FoodProductCategoryGetDto>();
        foreach (FoodProducts res in resList)
        {
            dtos.Add(new FoodProductCategoryGetDto
            {
                Id = res.Id,
                Name = res.Name,
                About = res.About,
                Price = res.Price,
                ImageId = res.ProductImageId,
                MainCategoryId = res.MainCategoryId,
                FoodProductCategoryId = res.FoodCategoryId,
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

        var res = FoodProductRepository
            .GetAllAsQueryable()
            .Where(p =>
                p.Name.uz.ToLower().Contains(query.ToLower())
            )
            .ToList();

        var dtos = res.Select(model => new FoodProductGetDto()
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