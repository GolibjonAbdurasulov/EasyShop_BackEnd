using API.Common;
using API.Controllers.CategoriesControllers.FoodProductCategoryController.Dtos;
using DatabaseBroker.Repositories.Categories.FoodProductCategoryRepository;
using Entity.Models.Product.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CategoriesControllers.FoodProductCategoryController;
[ApiController]
[Route("[controller]/[action]")]
public class FoodProductCategoryController : ControllerBase
{
    private IFoodProductCategoryRepository FoodProductCategoryRepository { get; set; }
    public FoodProductCategoryController(IFoodProductCategoryRepository foodProductCategoryRepository)
    {
        FoodProductCategoryRepository = foodProductCategoryRepository;
    }

    [HttpPost]
    [Authorize]
    public async Task<ResponseModelBase> CreateAsync( FoodProductCategoryCreationDto dto)
    {
        var entity = new FoodProductCategory
        {
            FoodProductCategoryName = dto.FoodProductCategoryName,
            FoodProductCategoryImageId =dto.FoodProductCategoryImageId,
        };
        var resEntity=await FoodProductCategoryRepository.AddAsync(entity);
        
        var resDto = new FoodProductCategoryGetDto
        {
            Id = resEntity.Id,
            FoodProductCategoryName = resEntity.FoodProductCategoryName,
            FoodProductCategoryImageId = resEntity.FoodProductCategoryImageId,
        };
        return new ResponseModelBase(resDto);
    }


  
    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateAsync( FoodProductCategoryUpdateDto dto)
    {
        var res =  await FoodProductCategoryRepository.GetByIdAsync(dto.Id);
        res.FoodProductCategoryImageId = dto.FoodProductCategoryImageId;
        res.FoodProductCategoryName = dto.FoodProductCategoryName;
        
        await FoodProductCategoryRepository.UpdateAsync(res);
        return new ResponseModelBase(dto);
    }
    
    
    [HttpDelete]
    [Authorize]
    public async Task<ResponseModelBase> DeleteAsync(long id)
    {
        var res =  await FoodProductCategoryRepository.GetByIdAsync(id);
        await FoodProductCategoryRepository.RemoveAsync(res);
        return new ResponseModelBase(res);
    }
    
    
    
    [HttpGet]
    public async Task<ResponseModelBase> GetByIdAsync(long id)
    {
        var resEntity =  await FoodProductCategoryRepository.GetByIdAsync(id);
        var dto = new FoodProductCategoryGetDto
        {
            Id = resEntity.Id,
            FoodProductCategoryName = resEntity.FoodProductCategoryName,
            FoodProductCategoryImageId = resEntity.FoodProductCategoryImageId,
        };
        return new ResponseModelBase(dto);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllAsync()
    {
        var res =   FoodProductCategoryRepository.GetAllAsQueryable().ToList();
        List<FoodProductCategoryGetDto> dtos = new List<FoodProductCategoryGetDto>();
        foreach (FoodProductCategory model in res)
        {
            dtos.Add(new FoodProductCategoryGetDto
            {
                Id = model.Id,
                FoodProductCategoryName = model.FoodProductCategoryName,
                FoodProductCategoryImageId = model.FoodProductCategoryImageId,
            });
        }
        
        return new ResponseModelBase(dtos);
    } 
}