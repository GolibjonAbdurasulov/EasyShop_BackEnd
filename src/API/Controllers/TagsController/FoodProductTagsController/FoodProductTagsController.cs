using API.Common;
using API.Controllers.TagsController.FoodProductTagsController.Dtos;
using DatabaseBroker.Repositories.Tags.FoodProductTagsRepository;
using Entity.Models.Product.Tags;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.TagsController.FoodProductTagsController;
[ApiController]
[Route("[controller]/[action]")]
public class FoodProductTagsController : ControllerBase
{
   private IFoodProductTagsRepository FoodProductTagsRepository { get; set; }
    public FoodProductTagsController(IFoodProductTagsRepository foodProductTagsRepository)
    {
        FoodProductTagsRepository = foodProductTagsRepository;
    }

    [HttpPost]
    [Authorize]
    public async Task<ResponseModelBase> CreateAsync( FoodProductTagsCreationDto dto)
    {
        var entity = new FoodProductTags
        {
            TagName = dto.TagName,
            CategoryId = dto.CategoryId,
        };
        var resEntity=await FoodProductTagsRepository.AddAsync(entity);
        
        var resDto = new FoodProductTagsDto
        {
            Id = resEntity.Id,
            TagName = resEntity.TagName,
            CategoryId = resEntity.CategoryId
        };
        return new ResponseModelBase(resDto);
    }


  
    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateAsync( FoodProductTagsDto dto)
    {
        var res =  await FoodProductTagsRepository.GetByIdAsync(dto.Id);
        res.TagName = dto.TagName;
        res.CategoryId = dto.CategoryId;
        
        await FoodProductTagsRepository.UpdateAsync(res);
        return new ResponseModelBase(dto);
    }
    
    
    [HttpDelete]
    [Authorize]
    public async Task<ResponseModelBase> DeleteAsync(long id)
    {
        var res =  await FoodProductTagsRepository.GetByIdAsync(id);
        await FoodProductTagsRepository.RemoveAsync(res);
        return new ResponseModelBase(res);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetByIdAsync(long id)
    {
        var resEntity =  await FoodProductTagsRepository.GetByIdAsync(id);
        var dto = new FoodProductTagsDto
        {
            Id = resEntity.Id,
            TagName = resEntity.TagName,
            CategoryId = resEntity.CategoryId
        };
        return new ResponseModelBase(dto);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllAsync()
    {
        var res =   FoodProductTagsRepository.GetAllAsQueryable().ToList();
        List<FoodProductTagsDto> dtos = new List<FoodProductTagsDto>();
        foreach (FoodProductTags model in res)
        {
            dtos.Add(new FoodProductTagsDto
            {
                Id = model.Id,
                TagName = model.TagName,
                CategoryId = model.CategoryId
            });
        }
        
        return new ResponseModelBase(dtos);
    }  
}