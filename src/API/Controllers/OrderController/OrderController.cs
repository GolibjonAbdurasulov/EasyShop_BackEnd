using API.Common;
using API.Controllers.OrderController.Dtos;
using DatabaseBroker.Repositories.OrderRepositories;
using Entity.Enums;
using Entity.Models.Order;
using Entity.Models.Product.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.OrderController;
[ApiController]
[Route("[controller]/[action]")]
public class OrderController : ControllerBase
{
    private IOrderRepository OrderRepository { get; set; }
    public OrderController(IOrderRepository orderRepository)
    {
        OrderRepository = orderRepository;
    }

    [HttpPost]
    [Authorize]
    public async Task<ResponseModelBase> CreateAsync( OrderCreationDto dto)
    {
        var entity = new Order
        {
            ProductsIds = dto.ProductsIds,
            TotalPrice = dto.TotalPrice,
            OrderStatus = dto.OrderStatus,
            DeliveryDate = dto.DeliveryDate,
            CustomerId = dto.CustomerId,
            Client = null
        };
        var resEntity=await OrderRepository.AddAsync(entity);
        
        var resDto = new OrderGetDto
        {
            Id = resEntity.Id,
            ProductsIds = resEntity.ProductsIds,
            TotalPrice = resEntity.TotalPrice,
            OrderStatus = resEntity.OrderStatus,
            DeliveryDate = resEntity.DeliveryDate,
            CustomerId = resEntity.CustomerId,
            User = resEntity.Client
        };
        return new ResponseModelBase(resDto);
    }


  
    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateAsync( OrderUpdateDto dto)
    {
        var res =  await OrderRepository.GetByIdAsync(dto.Id);
        res.ProductsIds = dto.ProductsIds;
        res.TotalPrice = dto.TotalPrice;
        res.OrderStatus = dto.OrderStatus;
        res.DeliveryDate = dto.DeliveryDate;
        res.CustomerId = dto.CustomerId;
        
        await OrderRepository.UpdateAsync(res);
        return new ResponseModelBase(dto);
    }
    
    
    [HttpDelete]
    [Authorize]
    public async Task<ResponseModelBase> DeleteAsync(long id)
    {
        
        var res =  await OrderRepository.GetByIdAsync(id);
        await OrderRepository.RemoveAsync(res);
        return new ResponseModelBase(res);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetByIdAsync(long id)
    {
        var resEntity =  await OrderRepository.GetByIdAsync(id);
        var dto = new OrderGetDto()
        { 
            Id = resEntity.Id,
            ProductsIds = resEntity.ProductsIds,
            TotalPrice = resEntity.TotalPrice,
            OrderStatus = resEntity.OrderStatus,
            DeliveryDate = resEntity.DeliveryDate,
            CustomerId = resEntity.CustomerId,
            User = resEntity.Client
        };
        return new ResponseModelBase(dto);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllAsync()
    {
        var res =   OrderRepository.GetAllAsQueryable().ToList();
        List<OrderGetDto> dtos = new List<OrderGetDto>();
        foreach (Order model in res)
        {
            dtos.Add(new OrderGetDto()
            {
                Id = model.Id,
                ProductsIds = model.ProductsIds,
                TotalPrice = model.TotalPrice,
                OrderStatus = model.OrderStatus,
                DeliveryDate = model.DeliveryDate,
                CustomerId = model.CustomerId,
                User = model.Client
            });
        }
        
        return new ResponseModelBase(dtos);
    }   
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllUserOrdersAsync(long userId)
    {
        var model =   OrderRepository.GetAllAsQueryable().
            FirstOrDefault(item=>item.CustomerId==userId);
        if (model == null)
            throw new NullReferenceException("Order not found OrderController");
        
        var dto =new OrderGetDto()
        {
            Id = model.Id,
            ProductsIds = model.ProductsIds,
            TotalPrice = model.TotalPrice,
            OrderStatus = model.OrderStatus,
            DeliveryDate = model.DeliveryDate,
            CustomerId = model.CustomerId,
            User = model.Client
        };
        
        
        return new ResponseModelBase(dto);
    }   
    
    
}