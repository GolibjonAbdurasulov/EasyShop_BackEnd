using API.Common;
using API.Controllers.TagsController.OilProductTagsController.Dtos;
using DatabaseBroker.Repositories.Tags.OilProductTagsRepository;
using Entity.Models.Product.Tags;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.TagsController.OilProductTagsController;

[ApiController]
[Route("[controller]/[action]")]
public class OilProductTagsController : ControllerBase
{
   private IOilProductTagsRepository OilProductTagsRepository { get; set; }
    public OilProductTagsController(IOilProductTagsRepository oilProductTagsRepository)
    {
        OilProductTagsRepository = oilProductTagsRepository;
    }

    [HttpPost]
    [Authorize]
    public async Task<ResponseModelBase> CreateAsync( OilProductTagsCreationDto dto)
    {
        var entity = new OilProductTags()
        {
            TagName = dto.TagName,
        };
        var resEntity=await OilProductTagsRepository.AddAsync(entity);
        
        var resDto = new OilProductTagsDto
        {
            Id = resEntity.Id,
            TagName = resEntity.TagName,
        };
        return new ResponseModelBase(resDto);
    }


  
    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateAsync( OilProductTagsDto dto)
    {
        var res =  await OilProductTagsRepository.GetByIdAsync(dto.Id);
        res.TagName = dto.TagName;
        
        await OilProductTagsRepository.UpdateAsync(res);
        return new ResponseModelBase(dto);
    }
    
    
    [HttpDelete]
    [Authorize]
    public async Task<ResponseModelBase> DeleteAsync(long id)
    {
        var res =  await OilProductTagsRepository.GetByIdAsync(id);
        await OilProductTagsRepository.RemoveAsync(res);
        return new ResponseModelBase(res);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetByIdAsync(long id)
    {
        var resEntity =  await OilProductTagsRepository.GetByIdAsync(id);
        var dto = new OilProductTagsDto
        {
            Id = resEntity.Id,
            TagName = resEntity.TagName,
        };
        return new ResponseModelBase(dto);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllAsync()
    {
        var res =   OilProductTagsRepository.GetAllAsQueryable().ToList();
        List<OilProductTagsDto> dtos = new List<OilProductTagsDto>();
        foreach (OilProductTags model in res)
        {
            dtos.Add(new OilProductTagsDto
            {
                Id = model.Id,
                TagName = model.TagName,
            });
        }
        
        return new ResponseModelBase(dtos);
    }  
}