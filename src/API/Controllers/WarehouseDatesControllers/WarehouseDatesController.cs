using API.Common;
using API.Controllers.WarehouseDatesControllers.Dtos;
using DatabaseBroker.Repositories.WarehouseDatesRepositories;
using Entity.Models.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Dtos;
using Services.Services;

namespace API.Controllers.WarehouseDatesControllers;

[ApiController]
[Route("[controller]/[action]")]
public class WarehouseDatesController(IWarehouseDatesRepository warehouseDatatesRepository) : ControllerBase
{
    private  IWarehouseDatesRepository _warehouseDatatesRepository { get; set; } = warehouseDatatesRepository;


    [HttpPost]
    [Authorize]
    public async Task<ResponseModelBase> CreateAsync( WarehouseDatesCreationDto dto)
    {
        var res= await _warehouseDatatesRepository.AddAsync(new WarehouseDates
        {
            QuantityBoxes = dto.QuantityBoxes,
            QuantityPieces = dto.QuantityPieces,
            QuantityInOneBox = dto.QuantityInOneBox,
        });
        return new ResponseModelBase(res);
    }
    
    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateAsync( WarehouseDatesDto dto)
    {
        var res =await _warehouseDatatesRepository.GetByIdAsync(dto.Id);
        res.QuantityBoxes= dto.QuantityBoxes;
        res.QuantityPieces = dto.QuantityPieces;
        res.QuantityInOneBox = dto.QuantityInOneBox;
        
        var model =await _warehouseDatatesRepository.UpdateAsync(res);
        return new ResponseModelBase(model);
    }
    
    
    [HttpDelete]
    [Authorize]
    public async Task<ResponseModelBase> DeleteAsync(long id)
    {
        var model = await _warehouseDatatesRepository.GetByIdAsync(id);
        var res =await _warehouseDatatesRepository.RemoveAsync(model);
        return new ResponseModelBase(res);
    }
   
    [HttpGet]
    public async Task<ResponseModelBase> GetAsync(long id)
    {
        var res =await _warehouseDatatesRepository.GetByIdAsync(id);
        return new ResponseModelBase(res);
    }
    
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllAsync()
    {
        var res =  _warehouseDatatesRepository.GetAllAsQueryable().ToList();
        return new ResponseModelBase(res);
    }
}