using API.Common;
using API.Controllers.ProductsControllers.WaterAndDrinkController.Dtos;
using DatabaseBroker.Repositories.Products.WaterAndDrinksRepository;
using DatabaseBroker.Repositories.Tags.WaterAndDrinkTagsRepository;
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
    public WaterAndDrinkController(IWaterAndDrinksRepository ourTeamRepository, IWaterAndDrinkTagsRepository houseHoldProductTags)
    {
        this.WaterAndDrinksRepository = ourTeamRepository;
        WaterAndDrinkTagsRepository = houseHoldProductTags;
    }

    [HttpPost]
    [Authorize]
    public async Task<ResponseModelBase> CreateAsync( CreationDto dto)
    {
        var entity = new WaterAndDrinks()
        {
            Name = dto.Name,
            About = dto.About,
            Price = dto.Price,
            ProductImageId = dto.ImageId,
            MainCategoryId = dto.MainCategoryId,
            TagId = dto.TagId,
        };
        var resEntity=await WaterAndDrinksRepository.AddAsync(entity);
        
        var resDto = new GetDto
        {
            Id = resEntity.Id,
            Name = resEntity.Name,
            About = resEntity.About,
            Price = resEntity.Price,
            ImageId = resEntity.ProductImageId,
            Image = resEntity.Image,
            MainCategoryId = resEntity.MainCategoryId,
            MainCategory = resEntity.MainCategory,
            TagId = resEntity.TagId,
            Tag = resEntity.Tag
        };
        return new ResponseModelBase(resDto);
    }


  
    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateAsync( UpdateDto dto)
    {
        var res =  await WaterAndDrinksRepository.GetByIdAsync(dto.Id);
        res.Name = dto.Name;
        res.About = dto.About; 
        res.Price = dto.Price;
        res.ProductImageId = dto.ImageId;
        res.MainCategoryId = dto.MainCategoryId;
        res.TagId = dto.TagId;
        
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
        var dto = new GetDto
        { 
            Id = resEntity.Id,
            Name = resEntity.Name,
            About = resEntity.About,
            Price = resEntity.Price,
            ImageId = resEntity.ProductImageId,
            Image = resEntity.Image,
            MainCategoryId = resEntity.MainCategoryId,
            MainCategory = resEntity.MainCategory,
            TagId = resEntity.TagId,
            Tag = resEntity.Tag
        };
        return new ResponseModelBase(dto);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllAsync()
    {
        var res =   WaterAndDrinksRepository.GetAllAsQueryable().ToList();
        List<GetDto> dtos = new List<GetDto>();
        foreach (WaterAndDrinks model in res)
        {
            dtos.Add(new GetDto
            {
                Id = model.Id,
                Name = model.Name,
                About = model.About,
                Price = model.Price,
                ImageId = model.ProductImageId,
                Image = model.Image,
                MainCategory = model.MainCategory,
                TagId = model.TagId,
                Tag = model.Tag
            });
        }
        
        return new ResponseModelBase(dtos);
    }   
    
    
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllByTagsAsync(long tagId)
    {
        var res =   WaterAndDrinksRepository.GetAllAsQueryable().Where(item=>item.TagId==tagId).ToList();
        List<GetDto> dtos = new List<GetDto>();
        foreach (WaterAndDrinks model in res)
        {
            dtos.Add(new GetDto
            {
                Id = model.Id,
                Name = model.Name,
                About = model.About,
                Price = model.Price,
                ImageId = model.ProductImageId,
                Image = model.Image,
                MainCategory = model.MainCategory,
                TagId = model.TagId,
                Tag = model.Tag
            });
        }
        
        return new ResponseModelBase(dtos);
    }
    
}