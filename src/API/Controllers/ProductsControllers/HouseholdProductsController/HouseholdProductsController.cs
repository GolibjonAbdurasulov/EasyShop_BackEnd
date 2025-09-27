using API.Common;
using API.Controllers.ProductsControllers.HouseholdProductsController.Dtos;
using DatabaseBroker.Repositories.Products.HouseHoldProductsRepository;
using DatabaseBroker.Repositories.Tags.HouseHoldProductTagsRepository;
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
    public async Task<ResponseModelBase> CreateAsync( CreationDto dto)
    {
        var entity = new HouseholdProducts()
        {
            Name = dto.Name,
            About = dto.About,
            Price = dto.Price,
            ImageId = dto.ImageId,
            CategoryImageId = dto.CategoryImageId,
            CategoryId = dto.CategoryId,
            TagId = dto.TagId
        };
        var resEntity=await HoldProductsRepository.AddAsync(entity);
        
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
            FoodProductCategory = resEntity.HouseholdProductCategory,
            TagId = resEntity.TagId,
            Tag = resEntity.Tag
        };
        return new ResponseModelBase(resDto);
    }


  
    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateAsync( UpdateDto dto)
    {
        var res =  await HoldProductsRepository.GetByIdAsync(dto.Id);
        res.Name = dto.Name;
        res.About = dto.About; 
        res.Price = dto.Price;
        res.ImageId = dto.ImageId;
        res.CategoryImageId = dto.CategoryImageId;
        res.CategoryId = dto.CategoryId;
        res.TagId = dto.TagId;
        
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
        var res =  await HoldProductsRepository.GetByIdAsync(id);
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
            FoodProductCategory = res.HouseholdProductCategory,
            TagId = res.TagId,
            Tag = res.Tag
        };
        return new ResponseModelBase(dto);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllAsync()
    {
        var res =   HoldProductsRepository.GetAllAsQueryable().ToList();
        List<GetDto> dtos = new List<GetDto>();
        foreach (HouseholdProducts model in res)
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
                FoodProductCategory = model.HouseholdProductCategory,
                TagId = model.TagId,
                Tag = model.Tag
            });
        }
        
        return new ResponseModelBase(dtos);
    }   
    
    
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllByTagsAsync(long tagId)
    {
        var res =   HoldProductsRepository.GetAllAsQueryable().Where(item=>item.TagId==tagId).ToList();
        List<GetDto> dtos = new List<GetDto>();
        foreach (HouseholdProducts model in res)
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
                FoodProductCategory = model.HouseholdProductCategory,
                TagId = model.TagId,
                Tag = model.Tag
            });
        }
        
        return new ResponseModelBase(dtos);
    }
}