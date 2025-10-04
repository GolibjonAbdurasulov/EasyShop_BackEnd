using API.Common;
using API.Controllers.CartControllers.Dtos;
using API.Controllers.OrderController.Dtos;
using DatabaseBroker.Repositories.CartRepositories;
using Entity.Enums;
using Entity.Models.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CartControllers;

[ApiController]
[Route("[controller]/[action]")]
public class CartController : ControllerBase
{
   private ICartRepository CartRepository { get; set; }
    public CartController(ICartRepository cartRepository)
    {
        CartRepository = cartRepository;
    }

    [HttpPost]
    [Authorize]
    public async Task<ResponseModelBase> CreateAsync( CartCreationDto dto)
    {
        var entity = new Cart
        {
            ProductsId = dto.ProductsId,
            CustomerId = dto.CustomerId,
        };
        var resEntity=await CartRepository.AddAsync(entity);
        
        var resDto = new CartGetDto
        {
            Id = resEntity.Id,
            ProductsId = resEntity.ProductsId,
            CustomerId = resEntity.CustomerId,
            Customer = resEntity.Customer
        };
        return new ResponseModelBase(resDto);
    }


  
    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateAsync( CartUpdateDto dto)
    {
        var res =  await CartRepository.GetByIdAsync(dto.Id);
        res.ProductsId = dto.ProductsId;
        res.CustomerId = dto.CustomerId;
        
        await CartRepository.UpdateAsync(res);
        return new ResponseModelBase(dto);
    }
    
    
    [HttpDelete]
    [Authorize]
    public async Task<ResponseModelBase> DeleteAsync(long id)
    {
        var res =  await CartRepository.GetByIdAsync(id);
        await CartRepository.RemoveAsync(res);
        return new ResponseModelBase(res);
    }
    
    [HttpGet]
    [Authorize]
    public async Task<ResponseModelBase> GetByClientIdAsync(long customerId)
    {
        var resEntity =   CartRepository.GetAllAsQueryable().
            FirstOrDefault(item=>item.CustomerId == customerId);
        if (resEntity == null)
            throw new NullReferenceException("There is no such cart on CartController");
        var dto = new CartGetDto
        {
            Id = resEntity.Id,
            ProductsId = resEntity.ProductsId,
            CustomerId = resEntity.CustomerId,
            Customer = resEntity.Customer
        };
        return new ResponseModelBase(dto);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetByIdAsync(long id)
    {
        var resEntity =  await CartRepository.GetByIdAsync(id);
        var dto = new CartGetDto
        {
            Id = resEntity.Id,
            ProductsId = resEntity.ProductsId,
            CustomerId = resEntity.CustomerId,
            Customer = resEntity.Customer
        };
        return new ResponseModelBase(dto);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllAsync()
    {
        var res =   CartRepository.GetAllAsQueryable().ToList();
        List<CartGetDto> dtos = new List<CartGetDto>();
        foreach (Cart model in res)
        {
            dtos.Add(new CartGetDto
            {
                Id = model.Id,
                ProductsId = model.ProductsId,
                CustomerId = model.CustomerId,
                Customer = model.Customer
            });
        }
        
        return new ResponseModelBase(dtos);
    }   
    
    
}