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
    public async Task<ResponseModelBase> CreateAsync( CreationDto dto)
    {
        var entity = new FoodProducts
        {
            Name = dto.Name,
            About = dto.About,
            Price = dto.Price,
            ImageId = dto.ImageId,
            CategoryImageId = dto.CategoryImageId,
            CategoryId = dto.CategoryId,
            TagId = dto.TagId
        };
        var resEntity=await FoodProductRepository.AddAsync(entity);
        
        var resDto = new GetDto
        {
            Id = resEntity.Id,
            Name = resEntity.Name,
            About = resEntity.About,
            Price = resEntity.Price,
            ImageId = resEntity.ImageId,
            Image = resEntity.Image,
            CategoryImageId = resEntity.CategoryImageId,
            CategoryImage = resEntity.CategoryImage,
            FoodProductCategory = resEntity.FoodProductCategory,
            TagId = resEntity.TagId,
            Tag = resEntity.Tag
        };
        return new ResponseModelBase(resDto);
    }


  
    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateAsync( UpdateDto dto)
    {
        var res =  await FoodProductRepository.GetByIdAsync(dto.Id);
        res.Name = dto.Name;
        res.About = dto.About; 
        res.Price = dto.Price;
        res.ImageId = dto.ImageId;
        res.CategoryImageId = dto.CategoryImageId;
        res.CategoryId = dto.CategoryId;
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
        var dto = new GetDto
        {
            Id = res.Id,
            Name = res.Name,
            About = res.About,
            Price = res.Price,
            ImageId = res.ImageId,
            Image = res.Image,
            CategoryImageId = res.CategoryImageId,
            CategoryImage = res.CategoryImage,
            CategoryId = res.CategoryId,
            FoodProductCategory = res.FoodProductCategory,
            TagId = res.TagId,
            Tag = res.Tag
        };
        return new ResponseModelBase(dto);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllAsync()
    {
        var res =   FoodProductRepository.GetAllAsQueryable().ToList();
        List<GetDto> dtos = new List<GetDto>();
        foreach (FoodProducts model in res)
        {
            dtos.Add(new GetDto
            {
                Id = model.Id,
                Name = model.Name,
                About = model.About,
                Price = model.Price,
                ImageId = model.ImageId,
                Image = model.Image,
                CategoryImageId = model.CategoryImageId,
                CategoryImage = model.CategoryImage,
                CategoryId =model.CategoryId,
                FoodProductCategory = model.FoodProductCategory,
                TagId = model.TagId,
                Tag = model.Tag
            });
        }
        
        return new ResponseModelBase(dtos);
    }   
    
    
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllByTagsAsync(long tagId)
    {
        var res =   FoodProductRepository.GetAllAsQueryable().Where(item=>item.TagId==tagId).ToList();
        List<GetDto> dtos = new List<GetDto>();
        foreach (FoodProducts model in res)
        {
            dtos.Add(new GetDto
            {
                Id = model.Id,
                Name = model.Name,
                About = model.About,
                Price = model.Price,
                ImageId = model.ImageId,
                Image = model.Image,
                CategoryImageId = model.CategoryImageId,
                CategoryImage = model.CategoryImage,
                CategoryId =model.CategoryId,
                FoodProductCategory = model.FoodProductCategory,
                TagId = model.TagId,
                Tag = model.Tag
            });
        }
        
        return new ResponseModelBase(dtos);
    }   
}