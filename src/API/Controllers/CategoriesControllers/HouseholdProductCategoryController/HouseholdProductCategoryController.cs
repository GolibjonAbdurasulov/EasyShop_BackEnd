using API.Common;
using API.Controllers.CategoriesControllers.HouseholdProductCategoryController.Dtos;
using DatabaseBroker.Repositories.Categories.HouseHoldProductCategoryRepository;
using Entity.Models.Product.Categories;
using Entity.Models.Product.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CategoriesControllers.HouseholdProductCategoryController;
[ApiController]
[Route("[controller]/[action]")]
public class HouseholdProductCategoryController : ControllerBase
{
    private IHouseHoldProductCategoryRepository HouseHoldProductCategoryRepository { get; set; }
    public HouseholdProductCategoryController(IHouseHoldProductCategoryRepository houseHoldProductCategoryRepository)
    {
        HouseHoldProductCategoryRepository = houseHoldProductCategoryRepository;
    }

    [HttpPost]
    [Authorize]
    public async Task<ResponseModelBase> CreateAsync( HouseholdProductCategoryCreationDto dto)
    {
        var entity = new HouseholdProductCategory
        {
            HouseHoldProductCategoryName = dto.HouseholdProductCategoryName,
            HouseHoldCategoryImageId = dto.HouseholdProductCategoryImageId,
        };
        var resEntity=await HouseHoldProductCategoryRepository.AddAsync(entity);
        
        var resDto = new HouseholdProductCategoryGetDto
        {
            Id = resEntity.Id,
            HouseholdProductCategoryName = resEntity.HouseHoldProductCategoryName,
            HouseholdProductCategoryImageId = resEntity.HouseHoldCategoryImageId
        };
        return new ResponseModelBase(resDto);
    }


  
    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateAsync( HouseholdProductCategoryUpdateDto dto)
    {
        var res =  await HouseHoldProductCategoryRepository.GetByIdAsync(dto.Id);
        res.HouseHoldProductCategoryName = dto.HouseholdProductCategoryName;
        res.HouseHoldCategoryImageId = dto.HouseholdProductCategoryImageId;
        
        await HouseHoldProductCategoryRepository.UpdateAsync(res);
        return new ResponseModelBase(dto);
    }
    
    
    [HttpDelete]
    [Authorize]
    public async Task<ResponseModelBase> DeleteAsync(long id)
    {
        var res =  await HouseHoldProductCategoryRepository.GetByIdAsync(id);
        await HouseHoldProductCategoryRepository.RemoveAsync(res);
        return new ResponseModelBase(res);
    }
    
    
    [HttpGet]
    public async Task<ResponseModelBase> GetByIdAsync(long id)
    {
        var resEntity =  await HouseHoldProductCategoryRepository.GetByIdAsync(id);
        var dto = new HouseholdProductCategoryGetDto
        {
            Id = resEntity.Id,
            HouseholdProductCategoryName = resEntity.HouseHoldProductCategoryName,
            HouseholdProductCategoryImageId = resEntity.HouseHoldCategoryImageId
        };
        return new ResponseModelBase(dto);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllAsync()
    {
        var res =   HouseHoldProductCategoryRepository.GetAllAsQueryable().ToList();
        List<HouseholdProductCategoryGetDto> dtos = new List<HouseholdProductCategoryGetDto>();
        foreach (HouseholdProductCategory model in res)
        {
            dtos.Add(new HouseholdProductCategoryGetDto
            {
                Id = model.Id,
                HouseholdProductCategoryName = model.HouseHoldProductCategoryName,
                HouseholdProductCategoryImageId = model.HouseHoldCategoryImageId
            });
        }
        
        return new ResponseModelBase(dtos);
    } 
}