using API.Common;
using API.Controllers.TagsController.WaterAndDrinksTagsController.Dtos;
using DatabaseBroker.Repositories.Tags.WaterAndDrinkTagsRepository;
using Entity.Models.Product.Tags;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.TagsController.WaterAndDrinksTagsController;

[ApiController]
[Route("[controller]/[action]")]
public class WaterAndDrinksTagsController : ControllerBase
{
   private IWaterAndDrinkTagsRepository WaterAndDrinkTagsRepository { get; set; }
    public WaterAndDrinksTagsController(IWaterAndDrinkTagsRepository waterAndDrinkTagsRepository)
    {
        WaterAndDrinkTagsRepository = waterAndDrinkTagsRepository;
    }

    [HttpPost]
    [Authorize]
    public async Task<ResponseModelBase> CreateAsync( WaterAndDrinksTagsCreationDto dto)
    {
        var entity = new WaterAndDrinksTags
        {
            TagName = dto.TagName,
        };
        var resEntity=await WaterAndDrinkTagsRepository.AddAsync(entity);
        
        var resDto = new WaterAndDrinksTagsDto()
        {
            Id = resEntity.Id,
            TagName = resEntity.TagName,
        };
        return new ResponseModelBase(resDto);
    }


  
    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateAsync( WaterAndDrinksTagsDto dto)
    {
        var res =  await WaterAndDrinkTagsRepository.GetByIdAsync(dto.Id);
        res.TagName = dto.TagName;
        
        await WaterAndDrinkTagsRepository.UpdateAsync(res);
        return new ResponseModelBase(dto);
    }
    
    
    [HttpDelete]
    [Authorize]
    public async Task<ResponseModelBase> DeleteAsync(long id)
    {
        var res =  await WaterAndDrinkTagsRepository.GetByIdAsync(id);
        await WaterAndDrinkTagsRepository.RemoveAsync(res);
        return new ResponseModelBase(res);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetByIdAsync(long id)
    {
        var resEntity =  await WaterAndDrinkTagsRepository.GetByIdAsync(id);
        var dto = new WaterAndDrinksTagsDto
        {
            Id = resEntity.Id,
            TagName = resEntity.TagName,
        };
        return new ResponseModelBase(dto);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllAsync()
    {
        var res =   WaterAndDrinkTagsRepository.GetAllAsQueryable().ToList();
        List<WaterAndDrinksTagsDto> dtos = new List<WaterAndDrinksTagsDto>();
        foreach (WaterAndDrinksTags model in res)
        {
            dtos.Add(new WaterAndDrinksTagsDto
            {
                Id = model.Id,
                TagName = model.TagName,
            });
        }
        
        return new ResponseModelBase(dtos);
    }  
}