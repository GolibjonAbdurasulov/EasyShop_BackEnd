using API.Common;
using API.Controllers.ProductsControllers.FoodProductController.Dtos;
using DatabaseBroker.Repositories.Products.FoodProductRepository;
using DatabaseBroker.Repositories.Tags.FoodProductTagsRepository;
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
            TagId = dto.TagId
        };
        var resEntity=await FoodProductRepository.AddAsync(entity);
        
        var resDto = new FoodProductGetDto
        {
            Id = resEntity.Id,
            Name = resEntity.Name,
            About = resEntity.About,
            Price = resEntity.Price,
            ImageId = resEntity.ProductImageId,
            Image = resEntity.Image,
            MainCategoryId = resEntity.MainCategoryId,
            FoodProductCategoryId = resEntity.FoodCategoryId,
            FoodProductCategory = resEntity.FoodProductCategory,
            TagId = resEntity.TagId,
            Tag = resEntity.Tag
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
            Image = res.Image,
            MainCategoryId = res.MainCategoryId,
            MainCategory = res.MainCategory,
            FoodProductCategoryId = res.FoodCategoryId,
            FoodProductCategory = res.FoodProductCategory,
            TagId = res.TagId,
            Tag = res.Tag
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
                Image = res.Image,
                MainCategoryId = res.MainCategoryId,
                MainCategory = res.MainCategory,
                FoodProductCategoryId = res.FoodCategoryId,
                FoodProductCategory = res.FoodProductCategory,
                TagId = res.TagId,
                Tag = res.Tag
            });
        }
        
        return new ResponseModelBase(dtos);
    }   
    
    
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllByTagsAsync(long tagId)
    {
        var resList =   FoodProductRepository.GetAllAsQueryable().Where(item=>item.TagId==tagId).ToList();
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
                Image = res.Image,
                MainCategoryId = res.MainCategoryId,
                MainCategory = res.MainCategory,
                FoodProductCategoryId = res.FoodCategoryId,
                FoodProductCategory = res.FoodProductCategory,
                TagId = res.TagId,
                Tag = res.Tag
            });
        }
        
        return new ResponseModelBase(dtos);
    }   
}