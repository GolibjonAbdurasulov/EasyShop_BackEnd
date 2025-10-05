using API.Common;
using API.Controllers.CategoriesControllers.MainProductCategoryController.Dtos;
using DatabaseBroker.Repositories.Categories.MainProductCategoryRepository;
using Entity.Models.Product.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CategoriesControllers.MainProductCategoryController;
[ApiController]
[Route("[controller]/[action]")]
public class MainProductCategoryController : ControllerBase
{
    private IMainProductCategoryRepository MainProductCategoryRepository { get; set; }
    public MainProductCategoryController(IMainProductCategoryRepository cartRepository)
    {
        MainProductCategoryRepository = cartRepository;
    }

    [HttpPost]
    [Authorize]
    public async Task<ResponseModelBase> CreateAsync( MainCategoryCreationDto dto)
    {
        var entity = new MainProductCategories
        {
            MainCategoryName = dto.MainCategoryName,
            MainCategoryImageId = dto.MainCategoryImageId,
        };
        var resEntity=await MainProductCategoryRepository.AddAsync(entity);
        
        var resDto = new MainCategoryGetDto
        {
            Id = resEntity.Id,
            MainCategoryName = resEntity.MainCategoryName,
            MainCategoryImageId = resEntity.MainCategoryImageId,
        };
        return new ResponseModelBase(resDto);
    }


  
    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateAsync( MainCategoryUpdateDto dto)
    {
        var res =  await MainProductCategoryRepository.GetByIdAsync(dto.Id);
        res.MainCategoryName = dto.MainCategoryName;
        res.MainCategoryImageId = dto.MainCategoryImageId;
        
        await MainProductCategoryRepository.UpdateAsync(res);
        return new ResponseModelBase(dto);
    }
    
    
    [HttpDelete]
    [Authorize]
    public async Task<ResponseModelBase> DeleteAsync(long id)
    {
        var res =  await MainProductCategoryRepository.GetByIdAsync(id);
        await MainProductCategoryRepository.RemoveAsync(res);
        return new ResponseModelBase(res);
    }
    
    
    [HttpGet]
    public async Task<ResponseModelBase> GetByIdAsync(long id)
    {
        var resEntity =  await MainProductCategoryRepository.GetByIdAsync(id);
        var dto = new MainCategoryGetDto
        {
            Id = resEntity.Id,
            MainCategoryName = resEntity.MainCategoryName,
            MainCategoryImageId = resEntity.MainCategoryImageId,
        };
        return new ResponseModelBase(dto);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllAsync()
    {
        var res = MainProductCategoryRepository
            .GetAllAsQueryable()
            .OrderBy(x => x.Id)
            .ToList();

        List<MainCategoryGetDto> dtos = new List<MainCategoryGetDto>();
        foreach (MainProductCategories model in res)
        {
            dtos.Add(new MainCategoryGetDto
            {
                Id = model.Id,
                MainCategoryName = model.MainCategoryName,
                MainCategoryImageId = model.MainCategoryImageId,
            });
        }
        
        return new ResponseModelBase(dtos);
    } 
}