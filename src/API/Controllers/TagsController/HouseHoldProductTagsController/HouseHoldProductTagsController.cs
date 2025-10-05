using API.Common;
using API.Controllers.TagsController.HouseHoldProductTagsController.Dtos;
using DatabaseBroker.Repositories.Tags.HouseHoldProductTagsRepository;
using Entity.Models.Product.Tags;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.TagsController.HouseHoldProductTagsController;

[ApiController]
[Route("[controller]/[action]")]
public class HouseHoldProductController : ControllerBase
{
    private IHouseHoldProductTagsRepository HouseHoldProductTagsRepository { get; set; }
    public HouseHoldProductController(IHouseHoldProductTagsRepository householdProductTagsRepository)
    {
        HouseHoldProductTagsRepository = householdProductTagsRepository;
    }

    [HttpPost]
    [Authorize]
    public async Task<ResponseModelBase> CreateAsync( HouseHoldProductTagsCreationDto dto)
    {
        var entity = new HouseholdProductTags
        {
            TagName = dto.TagName,
        };
        var resEntity=await HouseHoldProductTagsRepository.AddAsync(entity);
        
        var resDto = new HouseHoldProductTagsDto
        {
            Id = resEntity.Id,
            TagName = resEntity.TagName,
        };
        return new ResponseModelBase(resDto);
    }


  
    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateAsync( HouseHoldProductTagsDto dto)
    {
        var res =  await HouseHoldProductTagsRepository.GetByIdAsync(dto.Id);
        res.TagName = dto.TagName;
        
        await HouseHoldProductTagsRepository.UpdateAsync(res);
        return new ResponseModelBase(dto);
    }
    
    
    [HttpDelete]
    [Authorize]
    public async Task<ResponseModelBase> DeleteAsync(long id)
    {
        var res =  await HouseHoldProductTagsRepository.GetByIdAsync(id);
        await HouseHoldProductTagsRepository.RemoveAsync(res);
        return new ResponseModelBase(res);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetByIdAsync(long id)
    {
        var resEntity =  await HouseHoldProductTagsRepository.GetByIdAsync(id);
        var dto = new HouseHoldProductTagsDto
        {
            Id = resEntity.Id,
            TagName = resEntity.TagName,
        };
        return new ResponseModelBase(dto);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllAsync()
    {
        var res =   HouseHoldProductTagsRepository.GetAllAsQueryable().ToList();
        List<HouseHoldProductTagsDto> dtos = new List<HouseHoldProductTagsDto>();
        foreach (HouseholdProductTags model in res)
        {
            dtos.Add(new HouseHoldProductTagsDto
            {
                Id = model.Id,
                TagName = model.TagName,
            });
        }
        
        return new ResponseModelBase(dtos);
    }  
}