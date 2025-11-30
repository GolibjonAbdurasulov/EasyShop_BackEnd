using API.Common;
using API.Controllers.OrderController.Dtos;
using DatabaseBroker.Repositories.ClientRepository;
using DatabaseBroker.Repositories.OrderRepositories;
using DatabaseBroker.Repositories.Products.FoodProductRepository;
using DatabaseBroker.Repositories.Products.HouseHoldProductsRepository;
using DatabaseBroker.Repositories.Products.OilProductsRepository;
using DatabaseBroker.Repositories.Products.WaterAndDrinksRepository;
using Entity.Enums;
using Entity.Models.Order;
using Entity.Models.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.OrderController;
[ApiController]
[Route("[controller]/[action]")]
public class OrderController : ControllerBase
{
    private IOrderRepository OrderRepository { get; set; }
    private IOilProductsRepository OilProductsRepository { get; set; }
    private IFoodProductRepository FoodProducts { get; set; } 
    private IHouseHoldProductsRepository HouseholdProducts { get; set; }
    private IWaterAndDrinksRepository WaterAndDrinks { get; set; }
    private IClientRepository ClientRepository { get; set; }
    public OrderController(IOrderRepository orderRepository, IOilProductsRepository oilProductsRepository, 
        IFoodProductRepository foodProducts, IHouseHoldProductsRepository householdProducts, 
        IWaterAndDrinksRepository waterAndDrinks, IClientRepository clientRepository)
    {
        OrderRepository = orderRepository;
        OilProductsRepository = oilProductsRepository;
        FoodProducts = foodProducts;
        HouseholdProducts = householdProducts;
        WaterAndDrinks = waterAndDrinks;
        ClientRepository = clientRepository;
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
        var models =   OrderRepository.GetAllAsQueryable().
            Where(item=>item.CustomerId==userId).ToList();
        if (models == null)
            throw new NullReferenceException("Order not found OrderController");
        
        
        List<OrderGetDto> dtos = new List<OrderGetDto>();
        foreach (Order model in models)
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
    public async Task<ResponseModelBase> GetAllAcceptedOrders()
    {
        var models =   OrderRepository.GetAllAsQueryable().
            Where(item=>item.OrderStatus==OrderStatus.Accepted).ToList();
        if (models == null)
            throw new NullReferenceException("Order not found OrderController");
        
        
        List<OrderGetDto> dtos = new List<OrderGetDto>();
        foreach (Order model in models)
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
    public async Task<ResponseModelBase> GetAllShippedOrders()
    {
        var models =   OrderRepository.GetAllAsQueryable().
            Where(item=>item.OrderStatus==OrderStatus.Shipped).ToList();
        if (models == null)
            throw new NullReferenceException("Order not found OrderController");
        
        
        List<OrderGetDto> dtos = new List<OrderGetDto>();
        foreach (Order model in models)
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
    public async Task<ResponseModelBase> GetAllDeliveredOrders()
    {
        var models =   OrderRepository.GetAllAsQueryable().
            Where(item=>item.OrderStatus==OrderStatus.Delivered).ToList();
        if (models == null)
            throw new NullReferenceException("Order not found OrderController");
        
        
        List<OrderGetDto> dtos = new List<OrderGetDto>();
        foreach (Order model in models)
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
    public async Task<ResponseModelBase> GetAllPaidOrders()
    {
        var models =   OrderRepository.GetAllAsQueryable().
            Where(item=>item.OrderStatus==OrderStatus.Paid).ToList();
        if (models == null)
            throw new NullReferenceException("Order not found OrderController");
        
        
        List<OrderGetDto> dtos = new List<OrderGetDto>();
        foreach (Order model in models)
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
    public async Task<ResponseModelBase> GetAllUnPaidOrders()
    {
        var models =   OrderRepository.GetAllAsQueryable().
            Where(item=>item.OrderStatus==OrderStatus.Unpaid).ToList();
        if (models == null)
            throw new NullReferenceException("Order not found OrderController");
        
        
        List<OrderGetDto> dtos = new List<OrderGetDto>();
        foreach (Order model in models)
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
    public async Task<ResponseModelBase> GetOrderDetails(long orderId)
    {
        var model =   OrderRepository.GetAllAsQueryable().
            FirstOrDefault(item=>item.Id==orderId);
        if (model == null)
            throw new NullReferenceException("Order not found OrderController");
        var client = await ClientRepository.GetByIdAsync(model.CustomerId);

        
        List<OrderDetailsProducts> products = new List<OrderDetailsProducts>();
        foreach (ProductItem item in model.ProductsIds)
        {
            Product product = new Product();

            switch (item.ProductType)
            {
                case "OilProduct" :
                   product=await OilProductsRepository.GetByIdAsync(item.ProductId);
                    break;               
                case "HouseHoldProduct" :
                   product=await HouseholdProducts.GetByIdAsync(item.ProductId);
                    break;                
                case "FoodProduct" :
                   product=await FoodProducts.GetByIdAsync(item.ProductId);
                    break;                
                case "WaterAndDrinksProduct" :
                   product=await WaterAndDrinks.GetByIdAsync(item.ProductId);
                    break;
                default:
                product=null;
                    break;
            }
            
            products.Add(new OrderDetailsProducts
            {
                ProductId = item.ProductId,
                ProductName = product.Name.uz,
                Price = product.Price,
                Count = item.Quantity
            });
            product = null;
        }

        var res = new OrderDetailsGetDto
        {
            OrderId = model.Id,
            Status = model.OrderStatus,
            TotalPrice = model.TotalPrice,
            OrderedDate = model.DeliveryDate,
            OrderDetailsProducts = products,
            CustomerName = client.FullName,
            CustomerPhoneNumber = client.Email
        };


        return new ResponseModelBase(res);
    }


    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateShippedOrder(long  orderId)
    {
        var order= await OrderRepository.GetByIdAsync(orderId);
        if (order == null)
            throw new NullReferenceException("Order not found OrderController");
        
        order.OrderStatus = OrderStatus.Delivered;
        var res=await OrderRepository.UpdateAsync(order);
        return new ResponseModelBase(res);
    }

    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateAcceptedOrder(long orderId)
    {
        var order= await OrderRepository.GetByIdAsync(orderId);
        if (order == null)
            throw new NullReferenceException("Order not found OrderController");
        
        order.OrderStatus = OrderStatus.Shipped;
        order.DeliveryDate = DateTime.Now;
        var res=await OrderRepository.UpdateAsync(order);
        return new ResponseModelBase(res);
    }


    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateOrderStatus(long orderId,int status)
    {
        var order= await OrderRepository.GetByIdAsync(orderId);
        if (order == null)
            throw new NullReferenceException("Order not found OrderController");
        switch (status)
        {
            case 1:
                order.OrderStatus = OrderStatus.Paid;
                break;
            case 2:
                order.OrderStatus = OrderStatus.Unpaid;
                break;
            case 3:
                order.OrderStatus = OrderStatus.Shipped;
                break;
            case 4:
                order.OrderStatus = OrderStatus.Delivered;
                break;
            case 5:
                order.OrderStatus = OrderStatus.Accepted;
                break;
        }
        var res=await OrderRepository.UpdateAsync(order);
        return new ResponseModelBase(res);
    }


    [HttpPost]
    [Authorize]
    public async Task<ResponseModelBase> GetProductNames(GetProductDatas dto)
    {
        List<string> dtos = new List<string>();
        foreach (ProductItem item in dto.productsItemIds)
        {
            var data=await GetData(item.ProductType,item.ProductId);
            dtos.Add(data.Name.uz);
        }
        
        return new ResponseModelBase(dtos);
    }

    private async Task<Product> GetData(string type, long id)
    {
        switch (type)
        {
            case "FoodProduct":
                return await FoodProducts.GetByIdAsync(id);
            case "HouseHoldProduct": 
                return await HouseholdProducts.GetByIdAsync(id);
            case "WaterAndDrinksProduct": 
                return await WaterAndDrinks.GetByIdAsync(id);
            case "OilProduct":
                return await OilProductsRepository.GetByIdAsync(id);
            default:
                return null;
        }
    }
    


}